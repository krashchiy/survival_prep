Authors: Jane Tian, Nathan Robbins, Igor Ivanov

Repository:
https://github.com/krashchiy/survival_prep

Clone link:
https://github.com/krashchiy/survival_prep.git



References/External sources:
Fallout style css: https://codemyui.com/fallout-pip-boy-screen-in-css-and-html
Excel reader: https://github.com/ExcelDataReader/ExcelDataReader
Fuzzy string matcher: https://github.com/JakeBayer/FuzzySharp
Humanizer: https://github.com/Humanizr/Humanizer
Bootstrap: https://getbootstrap.com/docs/4.0/components/modal/
Sweet alerts: https://sweetalert2.github.io/

Our survival prep application provides fun and education in the same package.
Use your wits to solve fun trivia challenges and stock up on survival inventory!

		Features
1. Earn money for completing trivia challenges
2. Purchase survival items in the store
3. Get ranked on the score board by preparedness level for certain disaster types

		Quick guide
1. Register a user profile
2. Log into the website
3. Navigate to Challenges
	1. Get buried into thousands of fun and challenging trivia questions
	2. Select question difficulty (the higher the difficulty, the more money is earned for correct answer)
	3. Submit your answer to the question
	4. If an answer is correct, you have to proceed to next question. Otherwise, you can have an option of trying again or proceeding further.
	5. You can navigate away from challenges page at any time since the earned points are saved on the fly.
4. Navigate to Store
	1. Choose items and quantity needed for purchase
	2. Use earned money to buy the selected inventory
5. Navigate to HighScores
	1. Check your level of preparedness for specific disaster based on purchased inventory
	2. Check how you stack up against other users in the score table
	
		Internal features
1. AJAX handling for most of the requests for faster response times and smoother experience
2. Utilization of sweet alerts
3. Adding Fallout 4 theme styling
4. Trivia question import from excel using external package to read from multiple sheets over tens of thousands of records and import them all into database on the fly when the project first starts
5. Question answer match is performed using fuzzy string matching with Levenshtein distance.