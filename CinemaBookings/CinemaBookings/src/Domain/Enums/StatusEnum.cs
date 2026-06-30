
namespace CinemeBooking.Domain.Enums;

public enum StatusEnum
{
    // Trạng thái dùng chung cho Danh mục, Nhà cung cấp, Sản phẩm
    Inactive = 0,    // Ngừng hoạt động / Bị khóa / Ngừng kinh doanh
    Active = 1,      // Đang hoạt động / Đang bán kinh doanh /

    // Trạng thái nâng cao dành riêng cho Phiếu Nhập / Phiếu Xuất (Nếu cần)
    Draft = 2,       // Phiếu nháp (chưa trừ/cộng kho)
    Completed = 3,   // Đã hoàn thành (đã thực hiện xuất/nhập kho)
    Cancelled = 4    // Đã hủy phiếu
}