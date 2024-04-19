# GenerateUnitTestsWithAi.API
GenerateUnitTestsWithAi.API

## Generate UnitTests with ChatGpt using RapidAPI

You can use /transformation endpoints to define transformation for your code so the method text will be sent encoded<br />
All the transformation are saved to the /Data/CodingData.csv (check Csv.ExportFileRelativePath config key)

## Steps
1. add the method you want to unit test in the Data/FormatMe.txt file
1. call GET /Transformation/ReadMethodFromFile and copy the response
1. do POST /Transformation/Encode and use the previous response
1. copy the response and used in GET /UnitTest or directly in chatGPT chat
1. get the response and copy it in the FormatMe.txt and then again call GET /Transformation/ReadMethodFromFile
1. do POST /Transformation/Decode and use the previous response
1. now you have the tests, you just need to format it (just copy in your code and remove comments)

### to do:
#### add some unit tests
#### add method to apply transformation
#### add logs
#### add code for chatgpt
#### split to subprojects

### Links
https://rapidapi.com/rphrp1985/api/open-ai21/