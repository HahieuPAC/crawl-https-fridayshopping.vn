using CrawlDataWebsiteTool.Models;
using CrawlDataWebsiteToolBasic.Functions;
using CrawlDataWebsiteToolBasic.Helpers;
using Fizzler.Systems.HtmlAgilityPack;
using HtmlAgilityPack;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

/// 
/// <param name="currentPath"> Get curent path of project | Lấy đường dẫn của chương trình </param>
/// <param name="savePathExcel"> Path save excel file | Đường dẫn để lưu file excel </param>
/// <param name="baseUrl"> URL website need to crawl | Đường dẫn trang web cần crawl </param>
/// 


var currentPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) ?? "";
var savePathExcel = currentPath.Split("bin")[0] + @"Excel File\";
const string baseUrl = "https://www.hazzys.com";

//List mã loại sản phẩm
var typeCodes = new List<int>() { 10001, 10002, 10003, 10004, 10005};

// List product crawl
// List lưu danh sách các sản phẩm Crawl được
var listDataExport = new List<ProductModel>();

Console.WriteLine("Please do not turn off the app while crawling!");

//Loop
foreach (var typeCode in typeCodes)
{
    var requestUrl = baseUrl + $"/display.do?cmd=getTCategoryMain&TCAT_CD={typeCode}";
    Console.WriteLine(requestUrl);

    IWebDriver driver=new ChromeDriver();
    driver.Navigate().GoToUrl(requestUrl);
    var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(1000));

    var stopTime = DateTime.Now.AddMinutes(5);
        while (DateTime.Now < stopTime)
        {
            var elements = driver.FindElements(By.ClassName("pro-wrap__obj"));

            if (elements.Count > 0)
            {
                foreach (var element in elements)
                {
                    // tên sản phẩm
                    var nameProduct = element
                    .FindElement(By.ClassName("pro-name")).Text;


                    // Add Product to listDataExport
                    // Thêm sản phẩm vào listDataExport
                    listDataExport.Add(new ProductModel()
                    {
                        ProductName = nameProduct
                    });
                }
                break;
            }
        }

    driver.Close();
}
var fileName = DateTime.Now.Ticks + "Hayzzys-crawl.xlsx";


// Export data to Excel
ExportToExcel<ProductModel>.GenerateExcel(listDataExport, savePathExcel + fileName, "hayzzys-crawl");
