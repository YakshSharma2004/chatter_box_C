-- Create Database
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'C#project')
BEGIN
    CREATE DATABASE C#project;
END;
GO

USE C#project;
GO

-- Users Table

CREATE TABLE dbo.[user] (
    user_id INT IDENTITY(1,1) PRIMARY KEY,
    user_name VARCHAR(50) NOT NULL UNIQUE,
    user_pass VARCHAR(50) NOT NULL
);
GO

-- Channels Table

CREATE TABLE dbo.dis_channel (
    cha_id INT IDENTITY(1,1) PRIMARY KEY,
    channel_name VARCHAR(50) NOT NULL UNIQUE,
    channel_admin INT NOT NULL,
    CONSTRAINT fk_channel_admin FOREIGN KEY (channel_admin) 
        REFERENCES dbo.[user] (user_id) 
);
GO

-- User-Channel Mapping Table


CREATE TABLE dbo.dis_channel_user (
    id INT IDENTITY(1,1) PRIMARY KEY,
    dis_channel_chan_id INT NOT NULL,
    dis_channel_user_id INT NOT NULL,
    CONSTRAINT fk_dis_channel_user_chan_id FOREIGN KEY (dis_channel_chan_id) 
        REFERENCES dbo.dis_channel (cha_id) ,
    CONSTRAINT fk_dis_channel_user_user_id FOREIGN KEY (dis_channel_user_id) 
        REFERENCES dbo.[user] (user_id) 
);
GO

-- Chat Table


CREATE TABLE dbo.chat (
    chat_id INT IDENTITY(1,1) PRIMARY KEY,
    chat_timestamp DATETIME2(6) DEFAULT SYSUTCDATETIME(),
    chat_content VARCHAR(200) NOT NULL DEFAULT '-',
    channel_id INT NOT NULL,
    user_id INT NOT NULL,
    CONSTRAINT fk_chat_channel_id FOREIGN KEY (channel_id) 
        REFERENCES dbo.dis_channel (cha_id) ,
    CONSTRAINT fk_chat_user_id FOREIGN KEY (user_id) 
        REFERENCES dbo.[user] (user_id) 
);
GO

-- Friends Table


CREATE TABLE dbo.friend (
    friend_id INT IDENTITY(1,1) PRIMARY KEY,
    user_1_id INT NOT NULL,
    user_2_id INT NOT NULL,
    friend_since DATETIME2(6) DEFAULT SYSUTCDATETIME(),
    CONSTRAINT fk_friend_user_1 FOREIGN KEY (user_1_id) 
        REFERENCES dbo.[user] (user_id) ,
    CONSTRAINT fk_friend_user_2 FOREIGN KEY (user_2_id) 
        REFERENCES dbo.[user] (user_id) 
);
GO
alter table dbo.[user] add UserDescription varchar(100);

ALTER TABLE [user]
ADD DateOfJoin DATE;
ALTER TABLE [user]
ADD CONSTRAINT DF_User_DateOfJoin DEFAULT GETDATE() FOR DateOfJoin;

ALTER TABLE dbo.friend
ADD is_accepted BIT NOT NULL DEFAULT 0;