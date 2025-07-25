USE PRN222_EventManagement;
GO

-- USERS
INSERT INTO Users (name, email, password_hash, role)
VALUES
('Alice Johnson', 'alice@example.com', 'hash1', 'organizer'),
('Bob Smith', 'bob@example.com', 'hash2', 'attendee'),
('Carol White', 'carol@example.com', 'hash3', 'organizer'),
('David Brown', 'david@example.com', 'hash4', 'attendee'),
('Eva Green', 'eva@example.com', 'hash5', 'admin'),
('Frank Moore', 'frank@example.com', 'hash6', 'attendee'),
('Grace Lee', 'grace@example.com', 'hash7', 'attendee'),
('Helen Clark', 'helen@example.com', 'hash8', 'attendee'),
('Ian Scott', 'ian@example.com', 'hash9', 'organizer'),
('Jackie Chan', 'jackie@example.com', 'hash10', 'attendee'),
('Karen Adams', 'karen@example.com', 'hash11', 'attendee'),
('Luke Hall', 'luke@example.com', 'hash12', 'attendee'),
('Mona West', 'mona@example.com', 'hash13', 'organizer'),
('Nick Ford', 'nick@example.com', 'hash14', 'attendee'),
('Olivia Stone', 'olivia@example.com', 'hash15', 'attendee'),
('Paul Burns', 'paul@example.com', 'hash16', 'attendee'),
('Quincy Ray', 'quincy@example.com', 'hash17', 'organizer'),
('Rachel Wise', 'rachel@example.com', 'hash18', 'attendee'),
('Sam Young', 'sam@example.com', 'hash19', 'attendee'),
('Tina Bell', 'tina@example.com', 'hash20', 'attendee');

-- VENUES
INSERT INTO Venues (name, address, capacity)
VALUES
('Downtown Conference Center', '123 Main St, Cityville', 500),
('Tech Park Auditorium', '456 Innovation Blvd', 800),
('University Hall', '789 Campus Rd', 300),
('Grand Hotel Ballroom', '88 High St', 250),
('City Expo Hall', '99 Event Blvd', 1000);

-- EVENTS
INSERT INTO Events (title, description, organizer_id, venue_id, start_time, end_time, status)
VALUES
('Tech Summit 2025', 'Annual technology summit.', 1, 1, '2025-08-15 09:00', '2025-08-15 17:00', 'upcoming'),
('Startup Pitch Day', 'Showcase of startups.', 3, 2, '2025-07-10 10:00', '2025-07-10 14:00', 'upcoming'),
('AI in Education', 'AI in classrooms and e-learning.', 1, 3, '2025-06-01 10:00', '2025-06-01 13:00', 'completed'),
('Healthcare Innovation', 'Tech in health sector.', 9, 1, '2025-09-10 08:00', '2025-09-10 16:00', 'upcoming'),
('Blockchain Bootcamp', 'Blockchain & smart contracts.', 13, 4, '2025-07-01 10:00', '2025-07-01 17:00', 'upcoming'),
('Cybersecurity Forum', 'Trends in InfoSec.', 17, 5, '2025-10-05 09:00', '2025-10-05 18:00', 'upcoming'),
('Women in Tech', 'Empowerment and innovation.', 1, 3, '2025-06-20 10:00', '2025-06-20 16:00', 'completed'),
('Data Science Days', 'Data and AI.', 13, 2, '2025-07-15 10:00', '2025-07-16 17:00', 'upcoming'),
('Green Tech Expo', 'Sustainable technology.', 9, 5, '2025-08-05 10:00', '2025-08-05 16:00', 'upcoming'),
('FinTech Conference', 'Finance and technology.', 3, 4, '2025-09-25 09:00', '2025-09-25 17:00', 'upcoming');

-- TICKETS
INSERT INTO Tickets (event_id, type, price, quantity)
VALUES
-- Tech Summit
(1, 'Standard', 49.99, 200),
(1, 'VIP', 99.99, 50),
-- Startup Pitch
(2, 'General', 29.99, 150),
-- AI in Education
(3, 'Free', 0.00, 100),
-- Healthcare Innovation
(4, 'Standard', 59.99, 100),
(4, 'VIP', 109.99, 30),
-- Blockchain
(5, 'Early Bird', 39.99, 50),
(5, 'Regular', 59.99, 100),
-- Cybersecurity
(6, 'General', 69.99, 200),
-- Women in Tech
(7, 'Free', 0.00, 150),
-- Data Science Days
(8, '2-Day Pass', 119.99, 100),
-- Green Tech
(9, 'Regular', 39.99, 250),
-- FinTech
(10, 'General', 49.99, 200);

-- REGISTRATIONS
INSERT INTO Registrations (user_id, ticket_id, quantity)
VALUES
-- Tech Summit
(2, 1, 1),
(4, 1, 2),
(6, 2, 1),
(7, 1, 1),
(10, 1, 1),
-- Startup Pitch
(11, 3, 1),
(12, 3, 1),
-- AI in Education
(2, 4, 1),
(4, 4, 1),
(6, 4, 1),
-- Healthcare Innovation
(14, 5, 1),
(15, 6, 1),
-- Blockchain
(16, 7, 1),
(17, 8, 1),
-- Cybersecurity
(18, 9, 2),
(19, 9, 1),
-- Data Science
(20, 11, 1),
-- Green Tech
(2, 12, 1),
(5, 12, 1),
-- FinTech
(7, 13, 1);

-- SCHEDULE ITEMS (sample across multiple events)
INSERT INTO ScheduleItems (event_id, title, speaker, start_time, end_time)
VALUES
(1, 'Opening Keynote', 'Dr. Alan Turing', '2025-08-15 09:00', '2025-08-15 10:00'),
(1, 'Panel: Future of Tech', 'Various', '2025-08-15 10:30', '2025-08-15 12:00'),
(2, 'Startup Pitches', 'Various Founders', '2025-07-10 10:00', '2025-07-10 12:00'),
(3, 'AI in Classrooms', 'Prof. Ada Lovelace', '2025-06-01 10:00', '2025-06-01 11:30'),
(4, 'AI in Health', 'Dr. House', '2025-09-10 10:00', '2025-09-10 11:30'),
(5, 'Intro to Blockchain', 'Satoshi N.', '2025-07-01 10:00', '2025-07-01 12:00'),
(6, 'Cyber Threats 2025', 'CyberSec Guru', '2025-10-05 09:00', '2025-10-05 10:30'),
(7, 'Women Leaders Panel', 'Various', '2025-06-20 10:00', '2025-06-20 11:30'),
(8, 'Deep Learning Workshop', 'Dr. Data', '2025-07-15 11:00', '2025-07-15 13:00'),
(9, 'Green Startups Showcase', 'CleanTech Co.', '2025-08-05 10:00', '2025-08-05 12:00'),
(10, 'Future of Finance', 'BankTech Experts', '2025-09-25 09:00', '2025-09-25 11:00');

-- FEEDBACK
INSERT INTO Feedback (user_id, event_id, rating, comment)
VALUES
(2, 3, 5, 'Amazing seminar on AI.'),
(4, 3, 4, 'Well organized and informative.'),
(6, 3, 3, 'Good content, poor audio.'),
(14, 4, 5, 'Exciting medical innovation.'),
(15, 4, 4, 'Loved the keynote.');

-- Done
PRINT 'Sample data inserted successfully.';
