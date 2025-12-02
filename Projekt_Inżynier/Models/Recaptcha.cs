using System.Collections.Generic;

public class RecaptchaVerifyResponse
{
    public bool success { get; set; }                  
    public string challenge_ts { get; set; }          
    public string hostname { get; set; }              
    public List<string> error_codes { get; set; }    
}
