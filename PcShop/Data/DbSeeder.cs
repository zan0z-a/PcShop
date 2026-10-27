using Microsoft.AspNetCore.Identity;
using PcShop.Models;

namespace PcShop.Data
{
    public static class DbSeeder
    {
        private static string Img(string file)
        {
            return "https://commons.wikimedia.org/wiki/Special:FilePath/" + file + "?width=500";
        }

        public static void Seed(AppDbContext db)
        {
            if (!db.Users.Any(u => u.IsAdmin))
            {
                var admin = new User
                {
                    FirstName = "Админ",
                    LastName = "Главный",
                    Email = "admin@shop.ru",
                    IsAdmin = true
                };
                admin.PasswordHash = new PasswordHasher<User>().HashPassword(admin, "admin123");
                db.Users.Add(admin);
                db.SaveChanges();
            }

            if (db.Categories.Any())
            {
                return;
            }

            var cpu = new Category { Name = "Процессоры" };
            var gpu = new Category { Name = "Видеокарты" };
            var mb = new Category { Name = "Материнские платы" };
            var ram = new Category { Name = "Оперативная память" };
            var ssd = new Category { Name = "SSD накопители" };
            var pcCase = new Category { Name = "Корпуса" };

            db.Categories.AddRange(cpu, gpu, mb, ram, ssd, pcCase);
            db.SaveChanges();

            db.Products.AddRange(
                new Product { Name = "AMD Ryzen 9 7900X", Price = 38990, CategoryId = cpu.Id, ImageUrl = Img("AMD_Ryzen_9_7900X.jpg"), Description = "12 ядер, 24 потока, сокет AM5, частота до 5.6 ГГц, архитектура Zen 4." },
                new Product { Name = "AMD Ryzen 9 9950X", Price = 59990, CategoryId = cpu.Id, ImageUrl = Img("AMD_Ryzen_9_9950X.jpg"), Description = "16 ядер, 32 потока, сокет AM5, частота до 5.7 ГГц, архитектура Zen 5." },
                new Product { Name = "AMD Ryzen 5 2600", Price = 7490, CategoryId = cpu.Id, ImageUrl = Img("AMD_Ryzen_5_2600_(39851733273).jpg"), Description = "6 ядер, 12 потоков, сокет AM4, частота до 3.9 ГГц." },
                new Product { Name = "Gigabyte GeForce GTX 970 Windforce", Price = 12990, CategoryId = gpu.Id, ImageUrl = Img("Gigabyte_GTX_970_Windforce.jpg"), Description = "4 ГБ GDDR5, система охлаждения Windforce с тремя вентиляторами." },
                new Product { Name = "EVGA GeForce GTX 590", Price = 9990, CategoryId = gpu.Id, ImageUrl = Img("EVGA_GeForce_GTX_590.jpg"), Description = "Двухчиповая видеокарта, 3 ГБ GDDR5." },
                new Product { Name = "NVIDIA GeForce GTX 280", Price = 4990, CategoryId = gpu.Id, ImageUrl = Img("Gtx280.jpg"), Description = "1 ГБ GDDR3, интерфейс PCI-E 2.0." },
                new Product { Name = "Материнская плата ATX с кулером", Price = 8990, CategoryId = mb.Id, ImageUrl = Img("Atx_computer_motherboard_with_cpu_and_fan.jpg"), Description = "Форм-фактор ATX, 4 слота памяти, поддержка SATA." },
                new Product { Name = "MSI K7T266 Pro2", Price = 3490, CategoryId = mb.Id, ImageUrl = Img("MSI_K7T266_motherboard.jpg"), Description = "Форм-фактор ATX, сокет A, слот AGP." },
                new Product { Name = "Модуль памяти DDR4 8 ГБ", Price = 2490, CategoryId = ram.Id, ImageUrl = Img("RAM_Module_(SDRAM-DDR4).jpg"), Description = "DDR4, 8 ГБ, 3200 МГц, радиатор охлаждения." },
                new Product { Name = "Corsair Dominator Platinum DDR4 8 ГБ", Price = 4990, CategoryId = ram.Id, ImageUrl = Img("DDR4_Ram_IMGP5859_smial_wp.jpg"), Description = "DDR4, 8 ГБ, 2666 МГц, тайминги 15-17-17-35, подсветка." },
                new Product { Name = "Samsung 960 EVO 500 ГБ", Price = 5990, CategoryId = ssd.Id, ImageUrl = Img("Samsung_MZ-V6E500_m.2_SSD_IMGP5309_smial_wp.jpg"), Description = "M.2 2280, NVMe, скорость чтения до 3200 МБ/с." },
                new Product { Name = "NVMe SSD 1 ТБ 2280", Price = 6490, CategoryId = ssd.Id, ImageUrl = Img("1TB_2280_NVME_SSD.jpg"), Description = "M.2 2280, NVMe, объем 1 ТБ." },
                new Product { Name = "Fractal Design Focus G", Price = 6990, CategoryId = pcCase.Id, ImageUrl = Img("ATX_computer_case.jpg"), Description = "Корпус Midi-Tower, ATX, боковое окно, 2 вентилятора в комплекте." }
            );
            db.SaveChanges();
        }
    }
}