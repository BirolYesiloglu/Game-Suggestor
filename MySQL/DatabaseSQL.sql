-- Create the GameSuggestor database if not exists
CREATE DATABASE IF NOT EXISTS GameSuggestor;
USE GameSuggestor;

-- Create Genres table with an index on GenreName
CREATE TABLE IF NOT EXISTS Genres (
    GenreID INT AUTO_INCREMENT,
    GenreName VARCHAR(50),
    PRIMARY KEY (GenreID),
    INDEX idx_GenreName (GenreName)
);

-- Create Games table
CREATE TABLE IF NOT EXISTS Games (
    GameID INT AUTO_INCREMENT,
    Title VARCHAR(100),
    Platform VARCHAR(50),
    PRIMARY KEY (GameID)
);

-- Create GameGenres table
CREATE TABLE IF NOT EXISTS GameGenres (
    GameID INT,
    GenreID INT,
    FOREIGN KEY (GameID) REFERENCES Games(GameID),
    FOREIGN KEY (GenreID) REFERENCES Genres(GenreID)
);

CREATE TABLE IF NOT EXISTS Users (
    UserID INT AUTO_INCREMENT,
    Username VARCHAR(50) NOT NULL,
    Email VARCHAR(255) NOT NULL,
    Password VARCHAR(255) NOT NULL,
    PRIMARY KEY (UserID)
);

-- Create UserGamePreferences table
CREATE TABLE IF NOT EXISTS UserGamePreferences (
    UserID INT,
    GameID INT,
    Rating INT,
    PRIMARY KEY (UserID, GameID),
    FOREIGN KEY (UserID) REFERENCES Users(UserID),
    FOREIGN KEY (GameID) REFERENCES Games(GameID)
);

-- Create UserLogs table
CREATE TABLE IF NOT EXISTS UserLogs (
    LogID INT AUTO_INCREMENT,
    UserID INT,
    Timestamp TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    ActivityDescription VARCHAR(255),
    PRIMARY KEY (LogID),
    FOREIGN KEY (UserID) REFERENCES Users(UserID)
);

-- Insert data into Genres table
INSERT INTO Genres (GenreName)
VALUES
(''), ('Action'), ('Adventure'), ('RPG'), ('MMORPG'), ('Arcade'), 											-- 5
('FPS'), ('TPS'), ('Platformer'), ('Simulation'), ('Real-time strategy'), ('Sports'),						-- 11
('Fighting'), ('Shooter'), ('Puzzle'), ('Casual'), ('Strategy'), ('Stealth'),								-- 17
('Multiplayer'), ('Racing'), ('Tactical role-playing'), ('Survival'), ('Battle Royale'), 					-- 22
('Open World'), ('Horror');																					-- 24

-- Insert data into Games table
INSERT INTO Games (Title, Platform)
VALUES
('Call of Duty Game 1', 'Steam'),
('Call of Duty Game 2', 'Steam'),
('Call of Duty Game 3', 'Steam'),
('Halo Game 1', 'Steam'),
('Halo Game 2', 'Steam'),
('Halo Game 3', 'Steam'),
('Witcher Game 1', 'Steam'),
('Witcher Game 2', 'Steam'),
('Witcher Game 3', 'Steam'),
('Metro 2033', 'Steam'),
('Metro Last Night', 'Steam'),
('Metro Exodus', 'Steam');

-- Insert data into GameGenres table
INSERT INTO GameGenres (GameID, GenreID)
VALUES
(1,1), (2,1), (3,1),		-- Call of Duty games are NULL
(1, 7), (2, 7), (3, 7),   	-- Call of Duty games are FPS
(1, 2), (2, 2), (3, 2),   	-- Call of Duty games are Action
(1, 21), (2, 21), (3, 21),  -- Call of Duty games are Tactical role-playing
(4,1), (5,1), (6,1),		-- Halo games are NULL
(4, 8), (5, 8), (6, 8),  	-- Halo games are TPS
(4, 2), (5, 2), (6, 2),   	-- Halo games are Action
(4, 14), (5, 14), (6, 14),  -- Halo games are Shooter
(7,1), (8,1), (9,1),		-- Witcher games are NULL
(7, 4), (8, 4), (9, 4),   	-- Witcher games are RPG
(7, 24), (8, 24), (9, 24),  -- Witcher games are Open World
(7, 3), (8, 3), (9, 3),   	-- Witcher games are Adventure
(10,1), (11,1), (12,1),		-- Metro games are NULL
(10,2), (11,2), (12,2),		-- Metro games are Action
(10,14), (11,14), (12,14),	-- Metro games are Shooter
(10,7), (11,7), (12,7);		-- Metro games are FPS

-- Insert data into Users table
INSERT INTO Users (Username, Email, Password)
VALUES
('User1', 'user1@example.com', 'password1'),
('User2', 'user2@example.com', 'password2'),
('User3', 'user3@example.com', 'password3'),
('1','abc@hotmail.com','1');

-- Insert data into UserGamePreferences table
INSERT INTO UserGamePreferences (UserID, GameID, Rating)
VALUES
(1, 1, 4),
(2, 1, 5),
(1, 2, 5),
(1, 4, 3),
(2, 5, 2),
(2, 7, 4),
(3, 9, 5),
(3, 12, 1);

-- Insert sample user log entries
INSERT INTO UserLogs (UserID, ActivityDescription)
VALUES
(1, 'User1 logged in'),
(2, 'User2 viewed game suggestions'),
(3, 'User3 rated a game'),
(1, 'User1 updated preferences');

-- Display the contents of the Genres table
SELECT * FROM Genres;

-- Display the contents of the Games table
SELECT * FROM Games;

-- Display the contents of the GameGenres table
SELECT * FROM GameGenres;

-- Display the contents of the Users table
SELECT * FROM Users;

-- Display the contents of the UserGamePreferences table
SELECT * FROM UserGamePreferences;

-- Display the contents of the UserLogs table
SELECT * FROM UserLogs;


-- AGGREGATE FUNCTION
-- 1 Count the total number of games
-- SELECT COUNT(*) FROM Games;

-- 2 Find the average rating of a specific game
-- SELECT AVG(Rating) FROM UserGamePreferences WHERE GameID = 1;

-- 3 Find the maximum rating given to a specific game
-- SELECT MAX(Rating) FROM UserGamePreferences WHERE GameID = 1;

-- 4 Find the minimum rating given to a specific game
-- SELECT MIN(Rating) FROM UserGamePreferences WHERE GameID = 1;

-- 5 Count the number of games in each genre
-- SELECT Genres.GenreName, COUNT(GameGenres.GameID) 
-- FROM Genres 
-- JOIN GameGenres ON Genres.GenreID = GameGenres.GenreID 
-- GROUP BY Genres.GenreName;

-- 6 Find the most popular genre
-- SELECT Genres.GenreName, COUNT(GameGenres.GameID) as GameCount 
-- FROM Genres 
-- JOIN GameGenres ON Genres.GenreID = GameGenres.GenreID 
-- GROUP BY Genres.GenreName 
-- ORDER BY GameCount DESC 
-- LIMIT 1;

-- 7 Find the average rating for each game
-- SELECT Games.Title, AVG(UserGamePreferences.Rating) as AverageRating 
-- FROM Games 
-- JOIN UserGamePreferences ON Games.GameID = UserGamePreferences.GameID 
-- GROUP BY Games.Title;

-- 8 Find the total number of users who have rated each game
-- SELECT Games.Title, COUNT(UserGamePreferences.UserID) as UserCount 
-- FROM Games 
-- JOIN UserGamePreferences ON Games.GameID = UserGamePreferences.GameID 
-- GROUP BY Games.Title;

-- 9 Find the game with the highest average rating
-- SELECT Games.Title, AVG(UserGamePreferences.Rating) as AverageRating 
-- FROM Games 
-- JOIN UserGamePreferences ON Games.GameID = UserGamePreferences.GameID 
-- GROUP BY Games.Title 
-- ORDER BY AverageRating DESC 
-- LIMIT 1;

-- 10 Find the total number of activities logged by each user
-- SELECT Users.Username, COUNT(UserLogs.LogID) as ActivityCount 
-- FROM Users 
-- JOIN UserLogs ON Users.UserID = UserLogs.UserID 
-- GROUP BY Users.Username;