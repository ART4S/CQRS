INSERT INTO users (id, name, email, passwordhash, pictureid)
VALUES
('a91e29b7-813b-47a3-93f0-8ad34d4c8a09', 'Admin', 'admin@mail.com', 'FSohFrTszQ3CQ/LEpdcZgmMZ0QOZAx6YO+oNT3qO/SN/3Ij/ypJZrj9npuZttjVI', null),
('5687c80f-d495-460a-aae5-94ea8054ee2c', 'User', 'user@mail.com', 'FSohFrTszQ3CQ/LEpdcZgmMZ0QOZAx6YO+oNT3qO/SN/3Ij/ypJZrj9npuZttjVI', null);
	
INSERT INTO roles (id, name, description)
VALUES
('9bb11f1e-8b55-44e3-9137-2853ae9ebafd', 'Administrators', 'Администраторы'),
('8f4032ba-2bb8-4fa7-8ad8-393df9107590', 'Users', 'Пользователи');

INSERT INTO userroles (id, userid, roleid)
VALUES
('8d8af20f-fc3a-4f83-b2d3-68fafcd3b4df', 'a91e29b7-813b-47a3-93f0-8ad34d4c8a09', '9bb11f1e-8b55-44e3-9137-2853ae9ebafd'),
('cfe66686-6e3a-4606-831f-99e859fc32c4', '5687c80f-d495-460a-aae5-94ea8054ee2c', '8f4032ba-2bb8-4fa7-8ad8-393df9107590');

INSERT INTO rolepermissions (id, roleid, permission)
VALUES
('3c0e64a7-baf1-4071-bdfb-9ae655e1387e', '9bb11f1e-8b55-44e3-9137-2853ae9ebafd', 1),
('800c5e48-d56a-4529-b1c6-ae8c6296cc21', '9bb11f1e-8b55-44e3-9137-2853ae9ebafd', 2),
('973f17b1-f702-468f-a9a2-dc4558dae003', '9bb11f1e-8b55-44e3-9137-2853ae9ebafd', 3),
('500cf60b-8370-4e36-a750-c171d762373d', '9bb11f1e-8b55-44e3-9137-2853ae9ebafd', 4);

INSERT INTO countries (id, name, continent)
VALUES
('a29e1de6-4a7c-4e93-a7ac-87d35a5d4287', 'Russia', 'Asia'),
('ecd934c6-7fbf-4c18-83ed-d22ac0d2d119', 'USA', 'North America');

INSERT INTO cities (id, name, countryid)
VALUES
('b27a7a05-d61f-4559-9b04-5fd282a694d3', 'Kaliningrad', 'a29e1de6-4a7c-4e93-a7ac-87d35a5d4287'),
('b9f5c008-24da-49e2-bc83-b7356920881b', 'Newark', 'ecd934c6-7fbf-4c18-83ed-d22ac0d2d119');

INSERT INTO manufacturers (id, organizationname, homepageurl, streetaddress_streetname, streetaddress_postalcode, streetaddress_cityid)
VALUES
('278a79e9-5889-4953-a7c9-448c1e185600', 'BestManufacturer', null, 'ул. Пушкина д. Колотушкина', '111111', 'b27a7a05-d61f-4559-9b04-5fd282a694d3'),
('f5e7d6a2-c3c3-433c-93ec-adf4964bfcc8', 'WorstManufacturer', null, 'ул. Есенина д. Каруселина', '222222', 'b9f5c008-24da-49e2-bc83-b7356920881b');

INSERT INTO shippers (id, organizationname, headoffice_streetname, headoffice_postalcode, headoffice_cityid, contactphone)
VALUES
('49e075ee-7aed-4685-aed8-750483668c6d', 'FastDelivery', 'ул. Пушкина д. Колотушкина', '111111', 'b27a7a05-d61f-4559-9b04-5fd282a694d3', '8-800-555-35-35'),
('4705271e-39a1-465b-b4ff-bed8ea9c719a', 'SlowDelivery', 'ул. Есенина д. Каруселина', '222222', 'b9f5c008-24da-49e2-bc83-b7356920881b', '8-800-555-35-35');

INSERT INTO brands (id, name)
VALUES
('f612a3d0-573a-47e5-9f6b-a941f99fb26f', 'Nike'),
('3385b8ee-a5d6-4a8f-9f91-6c4c50d3628f', 'Puma');

INSERT INTO categories (id, name)
VALUES
('03e9c4b2-7587-4640-b376-2437414fd610', 'Trousers'),
('98d35a36-fc8f-4711-9211-a36c02a1e506', 'T-Shirts');

INSERT INTO products (id, name, price, description, createdate, manufacturerid, categoryid, brandid)
VALUES
('f321a9fa-fc44-47e9-9739-bb4d57724f3e', 'Nice trousers', 100, 'The best trousers in the world', '2020-05-18', '278a79e9-5889-4953-a7c9-448c1e185600', '03e9c4b2-7587-4640-b376-2437414fd610', 'f612a3d0-573a-47e5-9f6b-a941f99fb26f'),
('af8b5fe9-d32c-4897-a124-0ca3675939cf', 'Awesome t-shirt', null, null, '2020-05-18', 'f5e7d6a2-c3c3-433c-93ec-adf4964bfcc8', '98d35a36-fc8f-4711-9211-a36c02a1e506', '3385b8ee-a5d6-4a8f-9f91-6c4c50d3628f');

INSERT INTO productreviews (id, title, comment, createdate, rating, authorid, productid)
VALUES
('24e8157c-8b51-49d1-a3df-ee8f6ced17a8', 'The best trousers i have ever seen!!!', 'I''m gonna buy it again', '2020-05-18', 5, '5687c80f-d495-460a-aae5-94ea8054ee2c', 'f321a9fa-fc44-47e9-9739-bb4d57724f3e');

INSERT INTO productcomments (id, body, createdate, productid, authorid, parentcommentid)
VALUES
('c9502ede-2136-4202-8fe4-5e3d0006b0dc', 'The best', '2020-05-18', 'f321a9fa-fc44-47e9-9739-bb4d57724f3e', '5687c80f-d495-460a-aae5-94ea8054ee2c', null),
('02d1de1d-e5f3-4c94-870e-ec8b44fe4fd8', 'Thank you enjoying our products', '2020-05-18', 'f321a9fa-fc44-47e9-9739-bb4d57724f3e', 'a91e29b7-813b-47a3-93f0-8ad34d4c8a09', 'c9502ede-2136-4202-8fe4-5e3d0006b0dc');