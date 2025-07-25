-- Create Database
CREATE DATABASE PRN222_EventManagement;
GO

USE PRN222_EventManagement;
GO

-- USERS
CREATE TABLE Users (
    user_id INT PRIMARY KEY IDENTITY(1,1),
    name VARCHAR(100) NOT NULL,
    email VARCHAR(100) UNIQUE NOT NULL,
    password_hash VARCHAR(255) NOT NULL,
    role VARCHAR(20) CHECK (role IN ('attendee', 'organizer', 'admin')) NOT NULL,
    created_at DATETIME DEFAULT GETDATE()
);
GO

-- VENUES
CREATE TABLE Venues (
    venue_id INT PRIMARY KEY IDENTITY(1,1),
    name VARCHAR(100) NOT NULL,
    address TEXT NOT NULL,
    capacity INT NOT NULL
);
GO

-- EVENTS
CREATE TABLE Events (
    event_id INT PRIMARY KEY IDENTITY(1,1),
    title VARCHAR(200) NOT NULL,
    description TEXT,
    organizer_id INT NOT NULL,
    venue_id INT NOT NULL,
    start_time DATETIME NOT NULL,
    end_time DATETIME NOT NULL,
    status VARCHAR(20) CHECK (status IN ('upcoming', 'cancelled', 'completed')) NOT NULL,
    FOREIGN KEY (organizer_id) REFERENCES Users(user_id),
    FOREIGN KEY (venue_id) REFERENCES Venues(venue_id)
);
GO

-- TICKETS
CREATE TABLE Tickets (
    ticket_id INT PRIMARY KEY IDENTITY(1,1),
    event_id INT NOT NULL,
    type VARCHAR(50) NOT NULL,
    price DECIMAL(10,2) NOT NULL,
    quantity INT NOT NULL,
    FOREIGN KEY (event_id) REFERENCES Events(event_id)
);
GO

-- REGISTRATIONS
CREATE TABLE Registrations (
    registration_id INT PRIMARY KEY IDENTITY(1,1),
    user_id INT NOT NULL,
    ticket_id INT NOT NULL,
    quantity INT NOT NULL CHECK (quantity > 0),
    registered_at DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (user_id) REFERENCES Users(user_id),
    FOREIGN KEY (ticket_id) REFERENCES Tickets(ticket_id)
);
GO

-- SCHEDULE ITEMS
CREATE TABLE ScheduleItems (
    item_id INT PRIMARY KEY IDENTITY(1,1),
    event_id INT NOT NULL,
    title VARCHAR(200) NOT NULL,
    speaker VARCHAR(100),
    start_time DATETIME NOT NULL,
    end_time DATETIME NOT NULL,
    FOREIGN KEY (event_id) REFERENCES Events(event_id)
);
GO

-- FEEDBACK
CREATE TABLE Feedback (
    feedback_id INT PRIMARY KEY IDENTITY(1,1),
    user_id INT NOT NULL,
    event_id INT NOT NULL,
    rating INT CHECK (rating BETWEEN 1 AND 5),
    comment TEXT,
    submitted_at DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (user_id) REFERENCES Users(user_id),
    FOREIGN KEY (event_id) REFERENCES Events(event_id)
);
GO
