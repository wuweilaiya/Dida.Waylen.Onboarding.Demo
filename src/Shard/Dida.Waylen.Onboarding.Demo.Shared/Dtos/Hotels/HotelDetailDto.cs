using Dida.Waylen.Onboarding.Demo.Shared.Model.Dtos.Rooms; 

namespace Dida.Waylen.Onboarding.Demo.Shared.Model.Dtos.Hotels
{
    public class HotelDetailDto : HotelDto
    {
        /// <summary>
        /// 房间列表
        /// </summary>
        public List<RoomDto> Rooms { get; set; } = new();
    }
}
