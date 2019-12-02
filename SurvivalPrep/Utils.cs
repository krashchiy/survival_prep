using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ExcelDataReader;
using SurvivalPrep.DBModels;

namespace SurvivalPrep
{
    public static class Utils
    {
        public static void ImportTrivia(string fileName, PrepContext context)
        {
            DataSet ds = GetDatasetFromExcel(fileName);

            foreach (DataTable table in ds.Tables)
            {
                Score currentCatgory = (Score) Enum.Parse(typeof(Score), table.TableName.Trim());
                var category = new QuestionCategory
                {
                    CategoryName = table.TableName.Trim(),
                    ScoreWeight = (int) currentCatgory
                };

                context.QuestionCategories.Add(category);
                context.SaveChanges();

                foreach (DataRow row in table.Rows)
                {
                    //3 columns of quesiton/answer pairs per sheet
                    var question_col1 = row[0].ToString();
                    var question_col2 = row[3].ToString();
                    var question_col3 = row[6].ToString();

                    var answer_col1 = row[1].ToString();
                    var answer_col2 = row[4].ToString();
                    var answer_col3 = row[7].ToString();

                    if (!string.IsNullOrEmpty(question_col1) && !string.IsNullOrEmpty(answer_col1))
                    {
                        var question1 = new Question
                        {
                            QuestionCategoryId = category.QuestionCategoryId,
                            QuestionBody = question_col1,
                            Answer = answer_col1.SanitizeAnswer()
                        };

                        context.Questions.Add(question1);
                    }

                    if (!string.IsNullOrEmpty(question_col2) && !string.IsNullOrEmpty(answer_col2))
                    {
                        var question2 = new Question
                        {
                            QuestionCategoryId = category.QuestionCategoryId,
                            QuestionBody = question_col2,
                            Answer = answer_col2.SanitizeAnswer()
                        };

                        context.Questions.Add(question2);
                    }

                    if (!string.IsNullOrEmpty(question_col3) && !string.IsNullOrEmpty(answer_col3))
                    {
                        var question3 = new Question
                        {
                            QuestionCategoryId = category.QuestionCategoryId,
                            QuestionBody = question_col3,
                            Answer = answer_col3.SanitizeAnswer()
                        };

                        context.Questions.Add(question3);
                    }
                }

                context.SaveChanges();
            }
        }

        /// <summary>
        /// Extracts data from excel file into dataset.
        /// Reference: https://github.com/ExcelDataReader/ExcelDataReader
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private static DataSet GetDatasetFromExcel(string fileName)
        {
            using FileStream stream = File.Open(fileName, FileMode.Open, FileAccess.Read);
            using IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream);
            var config = new ExcelDataSetConfiguration
            {
                ConfigureDataTable = tableReader => new ExcelDataTableConfiguration { UseHeaderRow = true }
            };
            DataSet data = reader.AsDataSet(config);

            return data;
        }

        private static string SanitizeAnswer(this string input)
        {
            input = input.ToUpper().Replace("*CORRECT*", "");
            input = input.ToUpper().Replace("*CORRECT", "");

            //Ignore starting articles
            if (input.StartsWith("a ", StringComparison.OrdinalIgnoreCase))
            {
                input = input.Substring(2);
            }
            if (input.StartsWith("the ", StringComparison.OrdinalIgnoreCase))
            {
                input = input.Substring(4);
            }
            
            return input.Trim();
        }
    }
}
