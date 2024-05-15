CREATE TABLE users (
    id SERIAL PRIMARY KEY,
    firstName VARCHAR(50) NOT NULL,
    lastName VARCHAR(50) NOT NULL,
    username VARCHAR(50) UNIQUE NOT NULL,
    iban VARCHAR(50),
    email VARCHAR(100) UNIQUE NOT NULL,
    passwordHash VARCHAR(100) NOT NULL
);

CREATE TABLE roles (
    id SERIAL PRIMARY KEY,
    name VARCHAR(50) NOT NULL UNIQUE
);

CREATE TABLE userRoles (
    id SERIAL PRIMARY KEY,
    userId INTEGER REFERENCES users(id),
    roleId INTEGER REFERENCES roles(id),
    UNIQUE(userId, roleId)
);

CREATE TABLE company (
    id SERIAL PRIMARY KEY,
    name VARCHAR(100) NOT NULL,
    address TEXT,
    email VARCHAR(100),
    phone VARCHAR(20)
);

CREATE TABLE suppliers (
    id SERIAL PRIMARY KEY,
    name VARCHAR(100) NOT NULL,
    address TEXT,
    iban VARCHAR(50),
    email VARCHAR(100) UNIQUE
);

CREATE TABLE receipts (
    id SERIAL PRIMARY KEY,
	cashierId INTEGER REFERENCES users(id),
	companyId INTEGER REFERENCES company(id),
	paymentMethod VARCHAR(50),
    date TIMESTAMP NOT NULL
);

CREATE TABLE articles (
    id SERIAL PRIMARY KEY,
	supplierId INTEGER REFERENCES suppliers(id),
    name VARCHAR(100) NOT NULL,
    description TEXT,
    price NUMERIC(10, 2) NOT NULL,
    stockQuantity INTEGER DEFAULT 0
);

CREATE TABLE receiptItems (
    id SERIAL PRIMARY KEY,
    receiptId INTEGER REFERENCES receipts(id),
    articleId INTEGER REFERENCES articles(id) UNIQUE,
    quantity INTEGER NOT NULL
);


INSERT INTO users (firstName, lastName, username, iban, email, passwordHash) VALUES
('Admin', 'Administrator', 'admin', 'GB12X12345678901234', 'admin@fer.hr', '74ad9f35b0fe67dc691e690986345973addf4c95d93537c42c1a6043310f59c0'),
('John', 'Doe', 'johndoe', 'GB12X12345678901234', 'john@example.com', '58e978c141fe3bbafaeb0ade01cb82ff3c6a5e786bc21105f282e732dff943af'),
('Jane', 'Smith', 'janesmith', 'US34Y56789012345678', 'jane@example.com', 'e70d2259ee077230c18b340fa918ee2c8d7361e30d8e7a8abaf123c01eef3652');


INSERT INTO roles (name) VALUES
('administrator'),
('manager'),
('cashier');


INSERT INTO userRoles (userId, roleId) VALUES
(1, 1),
(2, 2),
(3,3);


INSERT INTO company (name, address, email, phone) VALUES
('Game Emporium', '123 Main St, City, Country', 'info@gameemporium.com', '+1234567890');


INSERT INTO suppliers (name, address, iban, email) VALUES
('Supplier A', '456 Oak St, City, Country', 'GB12X12345678901234', 'supplierA@example.com'),
('Supplier B', '789 Elm St, City, Country', 'US34Y56789012345678', 'supplierB@example.com');


INSERT INTO articles (name,supplierId,description, price, stockQuantity) VALUES
('Game 1', 1,'Description for Game 1', 49.99, 100),
('Game 2', 1,'Description for Game 2', 39.99, 50);


INSERT INTO receipts (cashierId, companyId,paymentMethod, date) VALUES
(1, 1,'Credit Card', '2024-04-13 10:15:00'),
(2, 1,'Cash', '2024-04-12 14:30:00'),
(1, 1,'Debit Card', '2024-04-11 09:45:00');


INSERT INTO receiptItems (receiptId, articleId, quantity, totalPrice) VALUES
(1, 1, 2, 99.98),
(1, 2, 1, 39.99),
(2, 2, 1, 39.99),
(3, 1, 2, 99.98);