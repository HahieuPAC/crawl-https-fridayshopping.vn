using CrawlDataWebsiteTool.Models;
using CrawlDataWebsiteToolBasic.Functions;
using CrawlDataWebsiteToolBasic.Helpers;
using Fizzler.Systems.HtmlAgilityPack;
using HtmlAgilityPack;
using System.Text;

/// <summary>
/// 
/// Data will be crawling from this website: https://fridayshopping.vn
/// You need to have basic knowledge with C#, Web HTML, CSS
/// If have any bug or question. Please comment in following this link: https://www.code-mega.com/p?q=crawl-data-trich-xuat-du-lieu-website-voi-c-phan-1-2c222jN
/// Advanced Tools here: https://www.code-mega.com/p?q=crawl-data-trich-xuat-du-lieu-website-voi-c-phan-2-72953tZ
/// 
/// </summary>
/// 
/// <param name="currentPath"> Get curent path of project | Lấy đường dẫn của chương trình </param>
/// <param name="savePathExcel"> Path save excel file | Đường dẫn để lưu file excel </param>
/// <param name="baseUrl"> URL website need to crawl | Đường dẫn trang web cần crawl </param>
/// 


var currentPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) ?? "";
var savePathExcel = currentPath.Split("bin")[0] + @"Excel File\";
const string baseUrl = "https://fridayshopping.vn";

// Create new instance
// Tạo một instance mới
var web = new HtmlWeb()
{
    AutoDetectEncoding = false,
    OverrideEncoding = Encoding.UTF8
};

//List mã loại sản phẩm
var typeCodes = new List<int>() { 10001, 10002, 10003, 10004, 10005 };

// List product crawl
// List lưu danh sách các sản phẩm Crawl được
var listDataExport = new List<ProductModel>();

// Spinner
var spinner = new ConsoleSpinner();

Console.WriteLine("Please do not turn off the app while crawling!");

//Loop
foreach (var typeCode in typeCodes)
{
    var requestUrl = baseUrl + $"/display.do?cmd=getTCategoryMain&TCAT_CD={typeCode}";

    // Load HTML to document from requestUrl
    // Load trang web, nạp html vào document từ requestUrl

    var documentForPagesTypeCode = web.Load(requestUrl);

    // Get all Node Product
    // Lấy tất cả các Node chứa thông tin của sản phẩm
    var listNodeProductItem = documentForPagesTypeCode
        .DocumentNode
        .QuerySelectorAll(".pro-wrap__obj")
        .ToList();

    Console.WriteLine(listNodeProductItem);
}
