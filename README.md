### **Test Online (hosted on Azure)**

STEPS:

(1. Visit to login: **https://sovtechapp.azurewebsites.net/Account/login** You will be redirected to microsoft to log in

(2. Enter username: **sovtechuser@pascal2bhotmail.onmicrosoft.com** and password: Mama6000

(3. Copy the value of **accessToken** generated from the login action.

(4. Visit: https://sovtechapp.azurewebsites.net/swagger/index.html 

(5. Login with the **accessToken** from step 3 by clicking on "Authorize" button. Enter: Bearer *<replace_with_accesstoken>*

(6. Test Apis

### **Test locally**

STEPS:

(1. Clone project 

(2. Run solution. docker-compose project is set as default and should be used in order to avoid changing port.

(3. Visit to login: **https://localhost:9100/Account/login** You will be redirected to microsoft to log in. NB: you may have to Change port

(4. Enter username: **sovtechuser@pascal2bhotmail.onmicrosoft.com** and password: Mama6000

(5. Copy the value of **accessToken** generated from the login action.

(6. Visit: https://localhost:9100/swagger/index.html

(7. Login with the **accessToken** from step 5 by clicking on "Authorize" button. Enter: Bearer *<replace_with_accesstoken>*

(8. Test Apis


### **CICD**

*will need to give the test account access to repo.*

CICD is set up to push the container to Azure container Registry. A webhook then deploys the container to Azure App Service.
To test CICD:
 - Edit code, for example, "WeatherForecastController"
 - Push to git, changes should reflect on the website
 
 If a unit test fails, deployment fails, and the API is unchanged.


#for questions, kindly send email to onuorahpascal@yahoo.com
