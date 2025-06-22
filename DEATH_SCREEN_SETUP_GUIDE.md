# Hướng dẫn Setup Death Screen

## Bước 1: Tạo Death Screen UI trong Scene

1. Trong Unity, mở scene bạn muốn thêm death screen (ví dụ: Scene1)
2. Tạo UI Canvas:
   - Chuột phải trong Hierarchy → UI → Canvas
   - Đặt tên là "DeathScreenCanvas"

3. Tạo Death Screen Panel:
   - Chuột phải vào Canvas → UI → Panel
   - Đặt tên là "DeathScreenPanel"
   - Đặt màu nền đen với alpha = 0.8 (hoặc màu bạn muốn)

4. Thêm Text "GAME OVER":
   - Chuột phải vào DeathScreenPanel → UI → Text
   - Đặt tên là "GameOverText"
   - Viết "GAME OVER" hoặc "YOU DIED"
   - Đặt font size lớn (ví dụ: 72)
   - Căn giữa text

5. Tạo Button "Play Again":
   - Chuột phải vào DeathScreenPanel → UI → Button
   - Đặt tên là "PlayAgainButton"
   - Thay đổi text thành "PLAY AGAIN"
   - Đặt vị trí phù hợp

6. Tạo Button "Quit":
   - Chuột phải vào DeathScreenPanel → UI → Button
   - Đặt tên là "QuitButton"
   - Thay đổi text thành "QUIT"
   - Đặt vị trí phù hợp

## Bước 2: Thêm Component vào DeathScreenPanel

1.  Trong Hierarchy, chọn `DeathScreenPanel`.
2.  **Quan trọng**: Đảm bảo `DeathScreenPanel` và `DeathScreenCanvas` đều được **đánh dấu tick (active)**.
3.  Trong Inspector của `DeathScreenPanel`, nhấn **Add Component**.
4.  Thêm script **DeathScreen**.
5.  Thêm component **Canvas Group**.
6.  Kéo thả các reference vào script `DeathScreen`:
    *   **Play Again Button**: kéo `PlayAgainButton` vào.
    *   **Quit Button**: kéo `QuitButton` vào.
    *   (Không còn trường `Death Screen Panel` nữa, vì script đã tự biết nó nằm trên panel nào).

## Bước 3: Thêm GameManager (Tự động)

GameManager sẽ được tạo tự động khi DeathScreen khởi tạo, nhưng bạn có thể tạo thủ công để chắc chắn:

1.  Tạo một GameObject trống trong scene.
2.  Đặt tên là "GameManager".
3.  Thêm component `GameManager` script.

## Bước 4: Thêm DeathScreenDebugger (Để debug)

1. Tạo một GameObject trống trong scene
2. Đặt tên là "DeathScreenDebugger"
3. Thêm component DeathScreenDebugger script
4. Chạy game và kiểm tra Console
5. Nhấn phím D để debug death screen thủ công

## Bước 5: Thêm DeathScreenTester (Tùy chọn)

1. Tạo một GameObject trống trong scene
2. Đặt tên là "DeathScreenTester"
3. Thêm component DeathScreenTester script
4. Test controls:
   - Nhấn phím T để test death screen
   - Nhấn phím R để reset death screen
   - Test nhiều lần để đảm bảo death screen hoạt động đúng

## Bước 6: Test

1. Chạy game
2. Kiểm tra Console để xem debug messages:
   - "DeathScreen found in scene!" = OK
   - "DeathScreen NOT found in scene!" = Cần setup lại
   - "GameManager: Player reset successfully" = OK
3. Test bằng cách:
   - Nhấn phím T để test death screen
   - Hoặc để player chết (mất hết máu)
4. Death screen sẽ hiện ra sau 2 giây
5. Test các button:
   - Play Again: quay về Scene1 với player reset
   - Quit: quay về MainMenu

## Troubleshooting

### Death Screen không hiện:
1. **Kiểm tra Console**: Xem có error messages không
2. **Kiểm tra DeathScreen component**: Đảm bảo đã thêm vào DeathScreenPanel
3. **Kiểm tra References**: Đảm bảo đã gán đúng các button và panel
4. **Test với phím T**: Sử dụng DeathScreenTester để test nhanh

### Player không reset khi Play Again:
1. **Kiểm tra GameManager**: Đảm bảo GameManager tồn tại trong scene
2. **Kiểm tra Console**: Xem có "Player reset successfully" không
3. **Kiểm tra PlayerHealth**: Đảm bảo PlayerHealth component có ResetPlayer method

### Lỗi vũ khí sau khi hồi sinh:
1. **MissingReferenceException**: Nếu gặp lỗi "ActiveWeapon has been destroyed but you are still trying to access it"
2. **Nguyên nhân**: ActiveWeapon.Instance bị hủy khi player chết nhưng vẫn được tham chiếu khi hồi sinh
3. **Giải pháp**: 
   - Đảm bảo PlayerHealth không hủy ActiveWeapon.Instance, chỉ hủy vũ khí hiện tại
   - GameManager sẽ tạo lại ActiveWeapon nếu cần khi reset game
   - ActiveInventory kiểm tra null trước khi truy cập ActiveWeapon.Instance

### Death Screen không hiện khi chết lần thứ 2:
1. **Nguyên nhân**: DeathScreen reference bị cũ hoặc trạng thái không được reset đúng cách
2. **Giải pháp**:
   - PlayerHealth tìm lại DeathScreen mỗi khi cần thiết thay vì chỉ tìm một lần
   - DeathScreen có method ResetDeathScreen() để reset trạng thái
   - GameManager gọi ResetDeathScreen() khi reset game
   - Đảm bảo Time.timeScale được reset về 1f

### Debug Messages quan trọng:
- ✅ "DeathScreen found in scene!" = Setup đúng
- ❌ "DeathScreen NOT found in scene!" = Chưa thêm component
- ❌ "deathScreenPanel is null!" = Chưa gán reference
- ✅ "DeathScreen: Panel shown and game paused" = Hoạt động đúng
- ✅ "GameManager: Player reset successfully" = Reset đúng
- ✅ "Player reset - Health: 3/3" = Player được reset
- ✅ "GameManager: Equipped starting weapon" = Vũ khí được trang bị lại
- ❌ "ActiveWeapon.Instance is null! Cannot change weapon." = Lỗi ActiveWeapon
- ✅ "PlayerHealth: Player is dying! Setting isDead = true" = Player bắt đầu chết
- ✅ "PlayerHealth: DeathRoutine started" = Death routine bắt đầu
- ✅ "PlayerHealth: DeathRoutine - DeathScreen found!" = Tìm thấy DeathScreen
- ❌ "PlayerHealth: DeathRoutine - DeathScreen is null!" = Không tìm thấy DeathScreen

### Vấn đề thường gặp:

**"DeathScreen NOT found in scene!" mặc dù đã thêm component:**
1. **Kiểm tra GameObject có active không**: Đảm bảo DeathScreenPanel không bị disable
2. **Kiểm tra component có enable không**: Đảm bảo DeathScreen script được enable
3. **Kiểm tra thứ tự script**: Có thể script chạy trước khi DeathScreen được khởi tạo
4. **Sử dụng DeathScreenDebugger**: Script này sẽ cho biết chính xác vấn đề

**Player vẫn trong trạng thái death khi Play Again:**
1. **Kiểm tra GameManager**: Đảm bảo GameManager tồn tại và hoạt động
2. **Kiểm tra ResetPlayer method**: Đảm bảo method được gọi đúng cách
3. **Kiểm tra Animator**: Đảm bảo animation được reset về Idle

**Không thể sử dụng vũ khí sau khi hồi sinh:**
1. **Kiểm tra ActiveWeapon**: Đảm bảo ActiveWeapon.Instance không bị hủy khi player chết
2. **Kiểm tra GameManager**: Đảm bảo GameManager tạo lại ActiveWeapon nếu cần
3. **Kiểm tra Player tag**: Đảm bảo player có tag "Player" để GameManager có thể tìm thấy

**Các bước kiểm tra nhanh:**
1. Chọn DeathScreenPanel trong Hierarchy
2. Kiểm tra Inspector xem có DeathScreen component không
3. Kiểm tra DeathScreen component có được enable không (checkbox bên cạnh tên component)
4. Kiểm tra GameObject có active không (checkbox bên cạnh tên GameObject)
5. Kiểm tra GameManager có tồn tại trong scene không
6. Kiểm tra Player có tag "Player" không

## Lưu ý

- DeathScreenPanel sẽ ẩn khi bắt đầu game
- Khi player chết, panel sẽ hiện ra và game sẽ pause (Time.timeScale = 0)
- Khi nhấn button, game sẽ resume (Time.timeScale = 1) và chuyển scene
- Player sẽ được reset hoàn toàn khi load scene mới (máu đầy, animation về Idle)
- Vũ khí sẽ được trang bị lại khi hồi sinh
- Nếu không tìm thấy DeathScreen, game sẽ fallback về load Scene1 

**Death Screen không hiện ngay từ lần chết đầu tiên:**
1. **Kiểm tra Console**: Xem có debug messages từ PlayerHealth không
2. **Kiểm tra CheckIfPlayerDeath**: Đảm bảo method được gọi khi player mất hết máu
3. **Kiểm tra DeathRoutine**: Đảm bảo coroutine được start
4. **Sử dụng DeathScreenDebugger**: Nhấn phím D để test death screen thủ công
5. **Kiểm tra thứ tự script**: Đảm bảo PlayerHealth không bị reset khi đã chết

**Các bước debug nhanh:**
1. Thêm DeathScreenDebugger vào scene
2. Chạy game và để player chết
3. Kiểm tra Console cho các messages:
   - "CheckIfPlayerDeath: currentHealth=0, isDead=false"
   - "PlayerHealth: Player is dying! Setting isDead = true"
   - "PlayerHealth: DeathRoutine started"
   - "PlayerHealth: DeathRoutine - DeathScreen found!"
4. Nếu không thấy messages, có vấn đề với logic damage/death
5. Nhấn phím D để test death screen thủ công 