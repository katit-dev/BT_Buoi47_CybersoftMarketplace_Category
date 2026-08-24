


public class HTTPResponseData<T>
{
    public T DataResponse { get; set; }
    public string Message { get; set; }
    public int statusCode { get; set; }
    public DateTime Timestamp { get; set; }
}


public static class UserResponseMessageDTO
{
    public const string SuccessRegister = "User registered successfully.";
    public const string FailedRegister = "User registration failed.";
    public const string EmailUsernameOrPhoneExists = "Email, username, or phone already exists.";
    public const string SuccessLogin = "User logged in successfully.";
    public const string InvalidLogin = "Invalid email, username, phone, or password.";
    public const string UserNotFound = "Username/Phone/Email or password is incorrect.";
}