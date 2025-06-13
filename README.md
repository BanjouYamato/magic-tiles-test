## Cách chạy project
1. Mở Unity Hub
2. Add project từ thư mục này
3. Dùng Unity version `2021.3.21f1` (hoặc version bạn đang dùng)
4. Mở scene `SampleScene.unity` và nhấn Play

## Design Choices
- Game được thiết kế dựa trên hệ thống thời gian rơi cố định: các tile được spawn từ một vị trí trục Y đã được xác định, rơi xuống một hit line được đặt trước.
- Từ khoảng cách giữa vị trí spawn và hit line, tôi tính được tốc độ rơi cố định, nhờ đó có thể xác định thời điểm spawn tile chính xác sao cho tile chạm đúng nhịp với bài hát.
- Hệ thống phân loại tile cũng được thiết kế đơn giản: tôi sử dụng nó ở 1 scene riêng không phải gameplay, bằng việc xử lý thủ công nhịp theo cảm giác bản thân, nhấn giữ tile > 0.25 giây thì tile đó được tính là long tile, còn ngắn hơn là tile thường, sau khi xong tôi sẽ lưu dữ liệu dưới dạng JSON để tiện sử dụng và tái sử dụng về sau.
- Dùng DOTween để tạo hiệu ứng nhẹ nhàng (nhấn, rung, combo)
- Tách riêng hệ thống âm thanh, score và tile để dễ mở rộng
- Tính điểm theo 3 mức: Perfect, Cool, Good

## Asset và Script từ bên ngoài
- DOTween (free từ Asset Store): dùng cho hiệu ứng UI
- Font: https://www.1001fonts.com/bronson-font.html
- SFX: https://assetstore.unity.com/packages/audio/sound-fx/free-casual-game-sfx-pack-54116