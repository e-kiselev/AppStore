using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

public class IndexModel : PageModel
{
    private readonly AppStoreController _appStoreController;

    public List<GameInfo> NewGames { get; set; }

    public IndexModel(AppStoreController appStoreController)
    {
        _appStoreController = appStoreController;
    }

    public async Task OnGet()
    {
        NewGames = await _appStoreController.FetchNewGamesFromiTunes();
    }

    public async Task OnGetSearch(string searchTerm)
    {
        // Реализуйте логику поиска, если необходимо
        // Может потребоваться изменить метод FetchNewGamesFromiTunes
        // для поддержки поиска
        NewGames = await _appStoreController.FetchNewGamesFromiTunes();
    }
}
