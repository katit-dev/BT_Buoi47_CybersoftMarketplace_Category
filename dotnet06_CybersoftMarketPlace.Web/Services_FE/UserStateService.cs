using Microsoft.JSInterop;
using System.Net.Http.Headers;

public class UserStateService
{
    // Service dùng để đọc/ghi LocalStorage của browser
    private readonly ILocalStorageService _localStorageService;

    // Lưu token tạm trong memory
    public string accessToken = "";

    // HttpClient gọi API Backend
    private readonly HttpClient _httpClient;


    public UserStateService(
        ILocalStorageService localStorageService,
        IHttpClientFactory httpClientFactory)
    {
        _localStorageService = localStorageService;

        // Tạo HttpClient đã cấu hình sẵn BaseAddress
        _httpClient = httpClientFactory
            .CreateClient("CybersoftMarketplaceApi");
    }


    // Event thông báo UI cập nhật lại state
    public Action OnChange { get; set; }
    public void StateHasChanged() => OnChange?.Invoke();

    public async Task LoginAsync(UserLoginDTO userLogin)
    {
        // 1. Gọi API Login Backend
        var response = await _httpClient
            .PostAsJsonAsync(
                "/api/User/Login",
                userLogin
            );


        // 2. Kiểm tra API trả về thành công
        if (response.IsSuccessStatusCode)
        {

            // 3. Đọc response JSON từ API
            var responseData =
                await response.Content
                .ReadFromJsonAsync<HTTPResponseData<string>>();


            if(responseData != null 
               && responseData.statusCode == 200)
            {

                // 4. Lấy JWT token
                accessToken = responseData.DataResponse;


                // 5. Lưu JWT vào LocalStorage browser
                await _localStorageService
                    .SetItemAsync(
                        "accessToken",
                        accessToken
                    );


                // 6. Gắn token vào HttpClient
                // Các request sau sẽ tự gửi:
                // Authorization: Bearer {token}

                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue(
                        "Bearer",
                        accessToken
                    );


                // 7. Báo UI login thành công
                StateHasChanged();
            }
        }
    }
}