namespace UnitTestProject1.Test_Globalcompanions
{

    public class LoginParam
    {
        public string __EVENTTARGET = "";
        public string __EVENTARGUMENT = "";
        public string __LASTFOCUS = "";
        public string __VIEWSTATE = "";
        public string __VIEWSTATEGENERATOR = "";
        public string ctl00_Header_cntrlLogin_txtBoxLogin = "";
        public string ctl00_Header_cntrlLogin_chkBoxRememberMe = "on";
        public string ctl00_Header_cntrlLogin_txtBoxPassword = "";
        public string ctl00_Header_cntrlLogin_btnLogin = "Enter";
        public string ctl00_cntrlHeaderMenu_drpLanguage = "1";
        public string ctl00_MAIN_ctl04_AgeFrom = "30";
        public string ctl00_MAIN_ctl04_AgeTo = "55";
        public string ctl00_MAIN_ctl04_ctrlCategorizedCoutries = "-1";

        public string ToPostData()
        {
            return DataEncode.EncodeData(string.Format("__EVENTTARGET={0}&__EVENTARGUMENT={1}&__LASTFOCUS={2}&__VIEWSTATE={3}&__VIEWSTATEGENERATOR={4}&ctl00$Header$cntrlLogin$txtBoxLogin={5}&ctl00$Header$cntrlLogin$chkBoxRememberMe={6}&ctl00$Header$cntrlLogin$txtBoxPassword={7}&ctl00$Header$cntrlLogin$btnLogin={8}&ctl00$cntrlHeaderMenu$drpLanguage={9}&ctl00$MAIN$ctl04$AgeFrom={10}&ctl00$MAIN$ctl04$AgeTo={11}&ctl00$MAIN$ctl04$ctrlCategorizedCoutries={12}", new object[] { this.__EVENTTARGET, this.__EVENTARGUMENT, this.__LASTFOCUS, this.__VIEWSTATE, this.__VIEWSTATEGENERATOR, this.ctl00_Header_cntrlLogin_txtBoxLogin, this.ctl00_Header_cntrlLogin_chkBoxRememberMe, this.ctl00_Header_cntrlLogin_txtBoxPassword, this.ctl00_Header_cntrlLogin_btnLogin, this.ctl00_cntrlHeaderMenu_drpLanguage, this.ctl00_MAIN_ctl04_AgeFrom, this.ctl00_MAIN_ctl04_AgeTo, this.ctl00_MAIN_ctl04_ctrlCategorizedCoutries }));
        }
    }
}

