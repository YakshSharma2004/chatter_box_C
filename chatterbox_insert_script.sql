-- Insert Users
INSERT INTO [user] ([user_name], [user_pass]) VALUES 
('Alice', 'password123'),
('Bob', 'securepass'),
('Charlie', 'charliePass');

-- Insert Channels (Admin is referenced by user ID)
INSERT INTO [dis_channel] ([channel_name], [channel_admin]) VALUES 
('General', 1), -- Alice is the admin
('Gaming', 2), -- Bob is the admin
('Coding', 3); -- Charlie is the admin

-- Insert User-Channel Relations
INSERT INTO [dis_channel_user] ([dis_channel_chan_id], [dis_channel_user_id]) VALUES 
(1, 1), -- Alice in General
(1, 2), -- Bob in General
(2, 2), -- Bob in Gaming
(2, 3), -- Charlie in Gaming
(3, 1), -- Alice in Coding
(3, 3); -- Charlie in Coding

-- Insert Chat Messages
INSERT INTO [chat] ([chat_content], [channel_id], [user_id]) VALUES 
('Hello, world!', 1, 1),
('How is everyone?', 1, 2),
('Who wants to play?', 2, 2),
('C# is awesome!', 3, 3);

-- Insert Friendships
INSERT INTO [friend] ([user_1_id], [user_2_id]) VALUES 
(1, 2), -- Alice and Bob are friends
(1, 3), -- Alice and Charlie are friends
(2, 3); -- Bob and Charlie are friends

UPDATE [user]
SET UserDescription = 'Frontend developer and team player.'
WHERE user_name = 'Alice';

UPDATE [user]
SET UserDescription = 'Gaming enthusiast and admin of the Gaming channel.'
WHERE user_name = 'Bob';

UPDATE [user]
SET UserDescription = 'Coder by day, coder by night. Loves C#.'
WHERE user_name = 'Charlie';

UPDATE [user]
SET DateOfJoin = '2023-01-10'
WHERE user_name = 'Alice';

UPDATE [user]
SET DateOfJoin = '2023-02-15'
WHERE user_name = 'Bob';

UPDATE [user]
SET DateOfJoin = '2023-03-20'
WHERE user_name = 'Charlie';
