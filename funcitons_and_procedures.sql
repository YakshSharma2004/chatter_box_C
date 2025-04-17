-- procedure for insert into chat table
create proc chat_insert 
@chatcon varchar(100),
@chanid int,
@userid int
as
insert into dbo.chat (chat_timestamp,chat_content,channel_id,user_id) values (GETDATE(),@chatcon,@chanid,@userid);

--procedure for getting all the friendsd of a user
CREATE PROCEDURE GetUserFriends
    @UserId INT
AS
BEGIN
    SELECT 
        u.user_id,
        u.user_name,
        u.UserDescription,
        u.DateOfJoin
    FROM [friend] f
    JOIN [user] u 
        ON (u.user_id = f.user_2_id AND f.user_1_id = @UserId)
        OR (u.user_id = f.user_1_id AND f.user_2_id = @UserId)
    WHERE u.user_id != @UserId;
END;

-- procedure to remove a friend by a user.
create proc remove_friend
@userid1 int,
@userid2 int
as 
delete friend where (user_1_id=@userid1 AND user_2_id=@userid2) OR (user_1_id=@userid2 AND user_2_id=@userid1);

-- procedure for adding a new user.

create proc addnewuser
@username varchar(50),
@user_pass varchar(50),
@userDes varchar(100)
as 
begin
insert into dbo.[user] ([user_name],[user_pass],[UserDescription]) values (@username,@user_pass,@userDes);
end
-- procedure for when there is a friend request sent by someone

CREATE OR ALTER PROC friend_req_sent 
    @user1 INT,
    @user2 INT
AS
BEGIN
    -- Check if the friend request already exists (in either direction)
    IF EXISTS (
        SELECT 1 FROM dbo.friend 
        WHERE (user_1_id = @user1 AND user_2_id = @user2)
           OR (user_1_id = @user2 AND user_2_id = @user1)
    )
    BEGIN
        -- Return custom message
        RAISERROR('Friend request already exists.', 16, 1);
        RETURN;
    END

    -- Insert the friend request
    INSERT INTO dbo.friend (user_1_id, user_2_id)
    VALUES (@user1, @user2);
END
-- procedure to join a new channel
create proc join_new_chl
@user_id int,
@channel_id int
as
begin
insert into dbo.dis_channel_user (dis_channel_chan_id,dis_channel_user_id)
values (@channel_id,@user_id);
end;

-- for getting the friend requests of a user
create proc get_friend_reqs
@user_id_logged int
as
select user_name,user_id from dbo.[user] u where u.user_id=
(select 
case 
when user_1_id=@user_id_logged then user_2_id
else
user_1_id
end
from dbo.friend where is_accepted=0 AND (user_1_id=@user_id_logged OR user_2_id=@user_id_logged));
