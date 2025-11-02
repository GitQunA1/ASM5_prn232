# Hướng dẫn Test Microservices với Postman

## Bước 1: Chuẩn bị môi trường

### 1.1. Khởi động RabbitMQ
```bash
docker run -d --hostname rabbitmq --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3-management
```
- Truy cập RabbitMQ Management UI: http://localhost:15672
- Username: `guest`
- Password: `guest`

### 1.2. Khởi động 3 Microservices trong Visual Studio
1. Chuột phải vào Solution → Properties
2. Chọn "Multiple startup projects"
3. Set Action = "Start" cho:
   - `EVRental.CheckOutQuanNhs.Microservices.QuanNH`
   - `EVRental.ReturnCondition.Microservices.QuanNH`
   - `EVRental.OcelotAPIGateway.QuanNH`
4. Click OK và nhấn F5 để chạy

Sẽ có 3 console windows mở ra:
- **CheckOut Service**: https://localhost:7050
- **ReturnCondition Service**: https://localhost:7229
- **Ocelot Gateway**: https://localhost:7021

---

## Bước 2: Test với Postman

### Option 1: Gọi qua Ocelot Gateway (Khuyến nghị - thấy đủ 3 logs)

**POST Request**
```
URL: https://localhost:7021/gateway/CheckOutQuanNh
Method: POST
Headers:
  Content-Type: application/json
```

**Request Body:**
```json
{
  "checkOutQuanNhid": 2,
  "checkOutTime": "2025-11-02T10:30:00",
  "returnDate": "2025-11-05",
  "extraCost": 20.0,
  "totalCost": 250.0,
  "lateFee": 0,
  "isPaid": true,
  "isDamageReported": false,
  "notes": "Test message via Ocelot Gateway",
  "customerFeedback": "Excellent service!",
  "paymentMethod": "Credit Card",
  "staffSignature": "John Doe",
  "customerSignature": "Jane Smith",
  "returnConditionId": 1
}
```

## Bước 3: Kiểm tra kết quả

### 3.1. Console Logs (sẽ thấy 3 dòng khi dùng Gateway):

**Console 1 - Ocelot Gateway:**
```
2025-11-02T10:45:30.1234567+07:00 *** GATEWAY RECEIVED *** POST /gateway/CheckOutQuanNh from client
```

**Console 2 - CheckOut Microservice:**
```
2025-11-02T10:45:30.2345678+07:00 *** PUBLISH *** data into CheckOutQuanNhQueue on RabbitMQ :: {"checkOutQuanNhid":2,"checkOutTime":"2025-11-02T10:30:00",...}
```

**Console 3 - ReturnCondition Microservice:**
```
2025-11-02T10:45:30.3456789+07:00 *** RECEIVE *** data from CheckOutQuanNhQueue on RabbitMQ :: {"checkOutQuanNhid":2,"checkOutTime":"2025-11-02T10:30:00",...}
```

### 3.2. RabbitMQ Management UI (http://localhost:15672)
1. Click tab "Queues"
2. Tìm queue `CheckOutQuanNhQueue`
3. Xem:
   - **Total messages**: số message đã qua queue
   - **Ready**: message chưa được consume (nếu ReturnCondition service không chạy)
   - **Unacked**: message đang được xử lý
   - Nếu cả 2 service đều chạy, message sẽ được consume ngay lập tức

### 3.3. Postman Response
```
Status: 200 OK
```

---
🎯 Tóm tắt workflow:

[Máy Windows - Visual Studio]
├── RabbitMQ (Docker) :5672
├── 3 Microservices (Multiple Startup)
│   ├── Ocelot Gateway :7021
│   ├── CheckOut Service :7050
│   └── ReturnCondition :7229
└── Android Emulator
    └── MAUI App → POST https://10.0.2.2:7021/gateway/CheckOutQuanNh

Khi nhấn nút trong app:

MAUI App (Emulator) 
  → 10.0.2.2:7021 (Gateway trên máy host)
    → Log console 1, 2, 3
      → RabbitMQ nhận message