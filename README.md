# Hướng dẫn chạy ứng dụng

Đây là hướng dẫn chi tiết để cài đặt và chạy ứng dụng của bạn trên môi trường phát triển local.

## Các bước chuẩn bị

### Bước 1: Cấu hình đường dẫn đến cơ sở dữ liệu

1. Mở file `AppDbContext` trong thư mục `Data` của dự án.
2. Tìm và chỉnh sửa chuỗi kết nối (connection string) đến cơ sở dữ liệu của bạn trong `appsettings.json`
   
   Ví dụ:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=localhost;Database=MyDatabase;User Id=myuser;Password=mypassword;"
   }

### Bước 2: Cập nhật cơ sở dữ liệu
Mở Package Manager Console trong Visual Studio.

Chọn dự án `AppData`

Chạy lệnh sau để cập nhật cơ sở dữ liệu: `Update-Database`

### Bước 3: Chạy API DataSeeder để chèn dữ liệu mẫu
Mở trình duyệt hoặc công cụ API như Postman.

Gọi API sau để chèn dữ liệu mẫu vào cơ sở dữ liệu: `GET /api/DataSeeder/seed`

API này sẽ tự động chèn dữ liệu vào các bảng của cơ sở dữ liệu.

### Bước 4: Cấu hình để chạy nhiều dự án cùng lúc (Multiple Startup Projects)
