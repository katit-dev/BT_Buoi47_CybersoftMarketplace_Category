
using System.Text.Json;
using dotnet06_CybersoftMarketPlace.Web.Pages;

public class ProductStateService
{
    private readonly HttpClient _httpClient;
    public List<ProductIndexPageDTO> Products { get; private set; } = new List<ProductIndexPageDTO>();

    public ProductStateService(HttpClient httpClient, IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("CybersoftMarketplaceApi");
    }




    public async Task LoadProductsAsync(string keyword = "", int pageIndex = 1, int pageSize = 10)
    {
        //Gọi api từ backend để lấy danh sách sản phẩm
        var response = await _httpClient.GetAsync($"/api/Product/GetAll?keyword={keyword}&pageIndex={pageIndex}&pageSize={pageSize}");
        if (response.IsSuccessStatusCode)
        {
            var responseData = await response.Content.ReadFromJsonAsync<HTTPResponseData<List<ProductIndexPageDTO>>>();
            if (responseData != null && responseData.statusCode == 200)
            {
                // Console.WriteLine($@"{JsonSerializer.Serialize(responseData.DataResponse)}");
                //Cập nhật api response data vào state management
                Products = responseData.DataResponse;
                StateHasChanged();
            }
        }
    }



    public Action OnChange { get; set; }

    public void StateHasChanged() => OnChange?.Invoke();

  
}