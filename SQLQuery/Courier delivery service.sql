CREATE DATABASE Courier_delivery_service;

USE Courier_delivery_service;

DROP DATABASE Courier_delivery_service;
/*
DROP TABLE courier_actions;
DROP TABLE client_actions;
DROP TABLE courier_transactions;
DROP TABLE client_transactions;
DROP TABLE admin_actions;
DROP TABLE payments;
DROP TABLE client_balances;
DROP TABLE courier_balances;
DROP TABLE order_status_history;
DROP TABLE orders;
DROP TABLE couriers;
DROP TABLE clients;
DROP TABLE admins;
DROP TABLE courier_auth;
DROP TABLE client_auth;
DROP TABLE admin_auth;
DROP TABLE products;
*/

/*
SELECT * FROM courier_actions;
SELECT * FROM client_actions;
SELECT * FROM courier_transactions;
SELECT * FROM client_transactions;
SELECT * FROM admin_actions;
SELECT * FROM payments;
SELECT * FROM client_balances;
SELECT * FROM courier_balances;
SELECT * FROM order_status_history;
SELECT * FROM orders;
SELECT * FROM couriers;
SELECT * FROM clients;
SELECT * FROM admins;
SELECT * FROM courier_auth;
SELECT * FROM client_auth;
SELECT * FROM admin_auth;
SELECT * FROM products;
*/
--UPDATE client_auth SET is_active = 1 WHERE client_id = 1;
--UPDATE courier_auth SET is_active = 1 WHERE courier_id = 1;
--UPDATE admin_auth SET is_active = 1 WHERE admin_id = 1;

INSERT INTO admins (admin_name, admin_phone)
VALUES ('asd', 'asd');
INSERT INTO admin_auth (admin_id, admin_name, admin_phone, admin_password)
VALUES (1, 'asd', 'asd', 'asd');
---------------------------------------------------
-- 1. Клиенты
CREATE TABLE clients (
    client_id INT PRIMARY KEY IDENTITY(1, 1),
    client_name VARCHAR(100) UNIQUE NOT NULL,
    client_phone VARCHAR(20) UNIQUE NOT NULL,
    email VARCHAR(100)
);

-- 2. Курьеры
CREATE TABLE couriers (
    courier_id INT PRIMARY KEY IDENTITY(1, 1),
    courier_name VARCHAR(100) UNIQUE NOT NULL,
    courier_phone VARCHAR(20) UNIQUE NOT NULL,
    vehicle VARCHAR(20) NOT NULL,
    courier_status VARCHAR(20) DEFAULT 'free'
);

-- 3. Администраторы
CREATE TABLE admins (
    admin_id INT PRIMARY KEY IDENTITY(1, 1),
    admin_name VARCHAR(100) UNIQUE NOT NULL,
    admin_phone VARCHAR(20) UNIQUE NOT NULL
);
---------------------------------------------------
-- 4. Заказы
CREATE TABLE orders (
    order_id INT PRIMARY KEY IDENTITY(1, 1),
    client_id INT NOT NULL,
    courier_id INT,
    created_date DATETIME DEFAULT GETDATE(),
    from_address TEXT NOT NULL,
    to_address TEXT NOT NULL,
    recipient_phone VARCHAR(20) NOT NULL,
    order_status VARCHAR(20) NOT NULL DEFAULT 'new',
    price DECIMAL(10,2),
    FOREIGN KEY (client_id) REFERENCES clients(client_id),
    FOREIGN KEY (courier_id) REFERENCES couriers(courier_id)
);
-- 5. Таблица для истории статусов заказов
CREATE TABLE order_status_history (
    history_id INT PRIMARY KEY IDENTITY(1, 1),
    order_id INT NOT NULL,
    courier_id INT,
    order_status VARCHAR(20) NOT NULL DEFAULT 'new',
    status_date DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (order_id) REFERENCES orders(order_id) ON DELETE CASCADE,
    FOREIGN KEY (courier_id) REFERENCES couriers(courier_id) ON DELETE CASCADE
);
-- 6. Таблица продуктов
CREATE TABLE products (
    product_id INT PRIMARY KEY IDENTITY(1, 1),
    product_name VARCHAR(100) NOT NULL,
    description TEXT,
    price DECIMAL(10,2) NOT NULL,
    category VARCHAR(50) NOT NULL,
    address TEXT NOT NULL,
    image_url VARCHAR(255),
    is_available BIT DEFAULT 1,
    created_date DATETIME DEFAULT GETDATE()
);
---------------------------------------------------
-- 7. Платежи
CREATE TABLE payments (
    payment_id INT PRIMARY KEY IDENTITY(1, 1),
    order_id INT NOT NULL,
    amount DECIMAL(10,2) NOT NULL,
    method VARCHAR(20) NOT NULL,
    payment_status VARCHAR(20) NOT NULL DEFAULT 'waiting',
    payment_date DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (order_id) REFERENCES orders(order_id) ON DELETE CASCADE
);
-- 8. Баланс пользователей
CREATE TABLE client_balances(
    client_balance_id INT PRIMARY KEY IDENTITY(1, 1),
    client_id INT NOT NULL,
    client_balance DECIMAL(10, 2) DEFAULT 0
    FOREIGN KEY (client_id) REFERENCES clients(client_id) ON DELETE CASCADE
);
-- 9. Баланс курьеров
CREATE TABLE courier_balances(
    courier_balance_id INT PRIMARY KEY IDENTITY(1, 1),
    courier_id INT NOT NULL,
    courier_balance DECIMAL(10, 2) DEFAULT 0
    FOREIGN KEY (courier_id) REFERENCES couriers(courier_id) ON DELETE CASCADE
);
---------------------------------------------------
-- 10. Действия клиентов
CREATE TABLE client_actions (
    action_id INT PRIMARY KEY IDENTITY(1, 1),
    client_id INT NOT NULL,
    order_id INT,
    client_action VARCHAR(50) NOT NULL, -- 'create_order', 'contact_support'
    action_date DATETIME DEFAULT GETDATE(),
    details TEXT,
    FOREIGN KEY (client_id) REFERENCES clients(client_id),
    FOREIGN KEY (order_id) REFERENCES orders(order_id)
);

-- 11. Действия курьеров
CREATE TABLE courier_actions (
    action_id INT PRIMARY KEY IDENTITY(1, 1),
    courier_id INT NOT NULL,
    order_id INT,
    courier_action VARCHAR(50) NOT NULL, -- 'accept_order', 'pickup_package', 'start_delivery', 'complete_delivery', 'contact_support'
    action_date DATETIME DEFAULT GETDATE(),
    notes TEXT,
    FOREIGN KEY (courier_id) REFERENCES couriers(courier_id),
    FOREIGN KEY (order_id) REFERENCES orders(order_id)
);

-- 12. Транзакции клиентов
CREATE TABLE client_transactions (
    client_transaction_id INT PRIMARY KEY IDENTITY(1, 1),
    client_id INT NOT NULL,
    userName VARCHAR(50) DEFAULT NULL,
    userId INT DEFAULT NULL,
    quantity DECIMAL(10, 2) NOT NULL,
    notes TEXT,
    date DATETIME DEFAULT GETDATE()
);

-- 13. Транзакции курьеров
CREATE TABLE courier_transactions (
    courier_transaction_id INT PRIMARY KEY IDENTITY(1, 1),
    courier_id INT NOT NULL,
    userName VARCHAR(50) DEFAULT NULL,
    userId INT DEFAULT NULL,
    quantity DECIMAL(10, 2) NOT NULL,
    notes TEXT,
    date DATETIME DEFAULT GETDATE()
);

-- 14. Действия администраторов
CREATE TABLE admin_actions (
    action_id INT PRIMARY KEY IDENTITY(1, 1),
    admin_id INT NOT NULL,
    admin_name VARCHAR(100) NOT NULL,
    selected_table TEXT,
    admin_action VARCHAR(50) NOT NULL, -- 'SELECT', 'INSERT', 'UPDATE', 'DELETE',
    action_data TEXT,
    action_date DATETIME DEFAULT GETDATE()
);
---------------------------------------------------
-- 15. Таблица для авторизации клиентов
CREATE TABLE client_auth (
    auth_id INT PRIMARY KEY IDENTITY(1, 1),
    client_id INT NOT NULL UNIQUE,
    client_name VARCHAR(100) NOT NULL UNIQUE,  -- логин (имя и фамилия)
    client_phone VARCHAR(100) NOT NULL UNIQUE,  -- логин (телефон)
    client_password VARCHAR(255) NOT NULL,    -- пароль
    created_date DATETIME DEFAULT GETDATE(),
    last_login DATETIME NULL,
    is_active INT DEFAULT 1,
    FOREIGN KEY (client_id) REFERENCES clients(client_id) ON DELETE CASCADE
);

-- 16. Таблица для авторизации курьеров
CREATE TABLE courier_auth (
    auth_id INT PRIMARY KEY IDENTITY(1, 1),
    courier_id INT NOT NULL UNIQUE,
    courier_name VARCHAR(100) NOT NULL UNIQUE,  -- логин (имя и фамилия)
    courier_phone VARCHAR(100) NOT NULL UNIQUE,  -- логин (телефон)
    courier_password VARCHAR(255) NOT NULL,    -- пароль
    created_date DATETIME DEFAULT GETDATE(),
    last_login DATETIME NULL,
    is_active INT DEFAULT 1,
    FOREIGN KEY (courier_id) REFERENCES couriers(courier_id) ON DELETE CASCADE
);

-- 17. Таблица для авторизации администраторов
CREATE TABLE admin_auth (
    auth_id INT PRIMARY KEY IDENTITY(1, 1),
    admin_id INT NOT NULL UNIQUE,
    admin_name VARCHAR(100) NOT NULL UNIQUE,  -- логин (имя и фамилия)
    admin_phone VARCHAR(100) NOT NULL UNIQUE,  -- логин (телефон)
    admin_password VARCHAR(255) NOT NULL,    -- пароль
    created_date DATETIME DEFAULT GETDATE(),
    last_login DATETIME NULL,
    is_active INT DEFAULT 1,
    FOREIGN KEY (admin_id) REFERENCES admins(admin_id) ON DELETE CASCADE
);
---------------------------------------------------
-- 1. Заполняем таблицу Клиентов
INSERT INTO clients (client_name, client_phone, email) VALUES
('Иван Петров', '+79161234567', 'ivan.petrov@mail.ru'),
('Мария Сидорова', '+79167654321', 'maria.sidorova@yandex.ru'),
('Алексей Козлов', '+79165554433', 'alex.kozlov@gmail.com'),
('Елена Васнецова', '+79168887766', 'elena.v@mail.ru'),
('Дмитрий Соколов', '+79163332211', 'dmitry.sokolov@yandex.ru');

---------------------------------------------------
-- 2. Заполняем таблицу Курьеров
INSERT INTO couriers (courier_name, courier_phone, vehicle, courier_status) VALUES
('Алексей Кузнецов', '+79165557788', 'car', 'free'),
('Дмитрий Васильев', '+79169991122', 'bicycle', 'free'),
('Сергей Иванов', '+79162223344', 'motorcycle', 'free'),
('Анна Морозова', '+79167778899', 'car', 'free'),
('Михаил Павлов', '+79164445566', 'bicycle', 'free');

---------------------------------------------------
-- 3. Заполняем таблицу Заказов
INSERT INTO orders (client_id, courier_id, created_date, from_address, to_address, recipient_phone, order_status, price) VALUES
(1, 1, DATEADD(day, -5, GETDATE()), 'ул. Ленина, д. 10, кв. 5, Москва', 'ул. Садовая, д. 15, офис 3, Москва', '+79163334455', 'delivered', 350.00),
(2, 2, DATEADD(day, -4, GETDATE()), 'пр. Мира, д. 25, кв. 12, Москва', 'ул. Центральная, д. 8, кв. 20, Москва', '+79167778899', 'delivered', 280.00),
(3, 3, DATEADD(day, -3, GETDATE()), 'ул. Садовая, д. 15, офис 3, Москва', 'пр. Вернадского, д. 105, кв. 45, Москва', '+79163332211', 'delivered', 420.00),
(1, 1, DATEADD(day, -2, GETDATE()), 'ул. Ленина, д. 10, кв. 5, Москва', 'ул. Тверская, д. 18, кв. 22, Москва', '+79167778899', 'delivered', 310.00),
(4, 4, DATEADD(day, -1, GETDATE()), 'ул. Центральная, д. 8, кв. 20, Москва', 'ул. Пушкина, д. 30, кв. 10, Москва', '+79165557788', 'in_transit', 190.00),
(5, 5, GETDATE(), 'пр. Вернадского, д. 105, кв. 45, Москва', 'ул. Гагарина, д. 12, кв. 5, Москва', '+79169991122', 'in_transit', 240.00),
(2, NULL, GETDATE(), 'пр. Мира, д. 25, кв. 12, Москва', 'ул. Арбат, д. 25, кв. 7, Москва', '+79164445566', 'new', 180.00);
---------------------------------------------------
-- 4. Заполняем таблицу Платежей
INSERT INTO payments (order_id, amount, method, payment_status, payment_date) VALUES
(1, 350.00, 'card', 'completed', DATEADD(day, -5, GETDATE())),
(2, 280.00, 'cash', 'completed', DATEADD(day, -4, GETDATE())),
(3, 420.00, 'online', 'completed', DATEADD(day, -3, GETDATE())),
(4, 310.00, 'card', 'completed', DATEADD(day, -2, GETDATE())),
(5, 190.00, 'cash', 'pending', NULL),
(6, 240.00, 'online', 'pending', NULL);

---------------------------------------------------
-- 5. Заполняем таблицу Действий клиентов
INSERT INTO client_actions (client_id, order_id, client_action, action_date, details) VALUES
(1, 1, 'create_order', DATEADD(day, -5, GETDATE()), 'Создан заказ на доставку документов'),
(2, 2, 'create_order', DATEADD(day, -4, GETDATE()), 'Заказ цветов на день рождения'),
(3, 3, 'create_order', DATEADD(day, -3, GETDATE()), 'Доставка электроники'),
(1, 4, 'create_order', DATEADD(day, -2, GETDATE()), 'Срочная доставка подарка'),
(4, 5, 'create_order', DATEADD(day, -1, GETDATE()), 'Доставка продуктов'),
(5, 6, 'create_order', GETDATE(), 'Документы в офис'),
(2, 7, 'create_order', GETDATE(), 'Заказ сувениров'),
(1, 1, 'contact_support', DATEADD(day, -5, GETDATE()), 'Уточнение времени доставки'),
(3, 3, 'contact_support', DATEADD(day, -3, GETDATE()), 'Как изменить номер телефона?');

---------------------------------------------------
-- 6. Заполняем таблицу Действий курьеров
INSERT INTO courier_actions (courier_id, order_id, courier_action, action_date, notes) VALUES
(1, 1, 'accept_order', DATEADD(day, -5, GETDATE()), 'Принял заказ в работу'),
(1, 1, 'pickup_package', DATEADD(hour, 1, DATEADD(day, -5, GETDATE())), 'Забрал посылку у отправителя'),
(1, 1, 'complete_delivery', DATEADD(hour, 2, DATEADD(day, -5, GETDATE())), 'Доставлено получателю лично'),

(2, 2, 'accept_order', DATEADD(day, -4, GETDATE()), 'Принял заказ с цветами'),
(2, 2, 'pickup_package', DATEADD(hour, 1, DATEADD(day, -4, GETDATE())), 'Забрал букет цветов'),
(2, 2, 'complete_delivery', DATEADD(hour, 3, DATEADD(day, -4, GETDATE())), 'Доставлено к торжеству'),

(3, 3, 'accept_order', DATEADD(day, -3, GETDATE()), 'Принял заказ с электроникой'),
(3, 3, 'pickup_package', DATEADD(hour, 2, DATEADD(day, -3, GETDATE())), 'Забрал технику'),
(3, 3, 'complete_delivery', DATEADD(hour, 4, DATEADD(day, -3, GETDATE())), 'Доставлено с проверкой'),

(1, 4, 'accept_order', DATEADD(day, -2, GETDATE()), 'Второй заказ от клиента'),
(1, 4, 'pickup_package', DATEADD(hour, 1, DATEADD(day, -2, GETDATE())), 'Забрал подарок'),
(1, 4, 'complete_delivery', DATEADD(hour, 2, DATEADD(day, -2, GETDATE())), 'Срочная доставка выполнена'),

(4, 5, 'accept_order', DATEADD(day, -1, GETDATE()), 'Принял заказ с продуктами'),
(4, 5, 'pickup_package', DATEADD(hour, 1, DATEADD(day, -1, GETDATE())), 'Забрал продукты из магазина'),
(4, 5, 'start_delivery', GETDATE(), 'В пути к получателю'),

(5, 6, 'accept_order', GETDATE(), 'Принял утренний заказ'),
(5, 6, 'pickup_package', DATEADD(minute, 30, GETDATE()), 'Забрал документы из офиса');
---------------------------------------------------
-- Создаем учетные записи для КЛИЕНТОВ
INSERT INTO client_auth (client_id, client_name, client_phone, client_password, is_active) VALUES
(1, 'Иван Петров', '+79161234567', 'ivan123', 1),
(2, 'Мария Сидорова', '+79167654321', 'maria456', 1),   
(3, 'Алексей Козлов', '+79165554433', 'alex789', 1),
(4, 'Елена Васнецова', '+79168887766', 'elena000', 1),
(5, 'Дмитрий Соколов', '+79163332211', 'dmitry111', 1);

---------------------------------------------------
-- Создаем учетные записи для КУРЬЕРОВ
INSERT INTO courier_auth (courier_id, courier_name, courier_phone, courier_password, is_active) VALUES
(1, 'Алексей Кузнецов', '+79165557788', 'courier2024', 1),
(2, 'Дмитрий Васильев', '+79169991122', 'delivery123', 1),
(3, 'Сергей Иванов', '+79162223344', 'express555', 1),
(4, 'Анна Морозова', '+79167778899', 'quick888', 1),
(5, 'Михаил Павлов', '+79164445566', 'fast999', 1);

-- Заполняем таблицу продуктов
INSERT INTO products (product_name, description, price, category, address, image_url) VALUES
('Samsung TV 55"', 'Телевизор Samsung 55 дюймов 4K UHD Smart TV', 45000.00, 'Электроника', 'г. Москва, ул. Тверская, д. 25, магазин "ТехноМир"', '/images/tv.jpg'),
('iPhone 15 Pro', 'Смартфон Apple iPhone 15 Pro 256GB', 89990.00, 'Электроника', 'г. Москва, пр. Мира, д. 12, салон связи "iStore"', '/images/iphone.jpg'),
('Coca-Cola 2л', 'Газированный напиток Coca-Cola 2 литра', 120.00, 'Еда', 'г. Москва, ул. Арбат, д. 45, супермаркет "Пятерочка"', '/images/cola.jpg'),
('Pepsi 1.5л', 'Газированный напиток Pepsi 1.5 литра', 110.00, 'Еда', 'г. Москва, ул. Новый Арбат, д. 18, гипермаркет "Ашан"', '/images/pepsi.jpg'),
('Хлеб Бородинский', 'Черный хлеб Бородинский нарезка 500г', 65.00, 'Еда', 'г. Москва, ул. Пресненская, д. 8, булочная "Хлебный дом"', '/images/bread.jpg'),
('Стул офисный', 'Офисный стул с регулировкой высоты', 2500.00, 'Мебель', 'г. Москва, ш. Энтузиастов, д. 32, мебельный центр "Мир мебели"', '/images/chair.jpg'),
('Диван угловой', 'Угловой диван с механизмом трансформации', 35000.00, 'Мебель', 'г. Москва, ул. Ленинградская, д. 15, салон "Евромебель"', '/images/sofa.jpg'),
('Футболка мужская', 'Хлопковая футболка мужская размер M', 800.00, 'Одежда', 'г. Москва, ТРЦ "Авиапарк", бутик "Zara"', '/images/tshirt.jpg'),
('Джинсы Levi''s', 'Джинсы Levi''s 501 классические', 5000.00, 'Одежда', 'г. Москва, ТЦ "Европейский", магазин "Levi''s Store"', '/images/jeans.jpg'),
('Кофе молотый', 'Кофе молотый Jacobs 250г', 450.00, 'Еда', 'г. Москва, ул. Большая Дмитровка, д. 10, кофейня "Кофехауз"', '/images/coffee.jpg'),
('Ноутбук HP', 'Ноутбук HP Pavilion 15.6" Core i5', 55000.00, 'Электроника', 'г. Москва, ул. Кузнецкий Мост, д. 7, магазин "Ситилинк"', '/images/laptop.jpg'),
('Микроволновка', 'Микроволновая печь Samsung 20л', 8000.00, 'Электроника', 'г. Москва, пр. Вернадского, д. 89, ТЦ "Горбушкин двор"', '/images/microwave.jpg');

INSERT INTO client_balances (client_id) VALUES (1), (2), (3), (4), (5);
INSERT INTO courier_balances (courier_id) VALUES (1), (2), (3), (4), (5);
---------------------------------------------------