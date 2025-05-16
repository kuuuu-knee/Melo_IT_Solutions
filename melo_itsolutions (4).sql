-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: May 16, 2025 at 06:08 PM
-- Server version: 10.4.32-MariaDB
-- PHP Version: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `melo_itsolutions`
--

-- --------------------------------------------------------

--
-- Table structure for table `admin`
--

CREATE TABLE `admin` (
  `admin_id` int(11) NOT NULL,
  `name` varchar(255) NOT NULL,
  `email` varchar(255) NOT NULL,
  `password` varchar(255) NOT NULL,
  `otp_code` varchar(10) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `admin`
--

INSERT INTO `admin` (`admin_id`, `name`, `email`, `password`, `otp_code`) VALUES
(5, 'Neco Clemente', 'necoclemente7@gmail.com', 'Admin1234', '');

-- --------------------------------------------------------

--
-- Table structure for table `departments`
--

CREATE TABLE `departments` (
  `department_id` int(11) NOT NULL,
  `department_name` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `departments`
--

INSERT INTO `departments` (`department_id`, `department_name`) VALUES
(7, 'Customer Support'),
(2, 'Human Resource'),
(1, 'Information Technology'),
(3, 'Marketing and Finance'),
(5, 'Operations'),
(6, 'Sales');

-- --------------------------------------------------------

--
-- Table structure for table `files`
--

CREATE TABLE `files` (
  `file_id` int(11) NOT NULL,
  `file_name` varchar(255) NOT NULL,
  `uploaded_by` int(11) NOT NULL,
  `upload_date` timestamp NOT NULL DEFAULT current_timestamp(),
  `task_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `files`
--

INSERT INTO `files` (`file_id`, `file_name`, `uploaded_by`, `upload_date`, `task_id`) VALUES
(1, 'wireframes.pdf', 1, '2025-02-01 15:34:33', 1),
(2, 'migration_script.sql', 2, '2025-02-01 15:34:33', 2),
(3, 'financial_report.xlsx', 3, '2025-02-01 15:34:33', 3),
(4, 'ad_design.png', 4, '2025-02-01 15:34:33', 4),
(5, 'route_plan.csv', 5, '2025-02-01 15:34:33', 5),
(6, 'sales_presentation.ppt', 6, '2025-02-01 15:34:33', 6),
(7, 'chatbot_code.zip', 7, '2025-02-01 15:34:33', 7),
(8, 'contract_review.docx', 8, '2025-02-01 15:34:33', 8),
(9, 'prototype_results.pdf', 9, '2025-02-01 15:34:33', 9),
(10, 'ai_model.h5', 10, '2025-02-01 15:34:33', 10);

-- --------------------------------------------------------

--
-- Table structure for table `logs`
--

CREATE TABLE `logs` (
  `log_id` int(11) NOT NULL,
  `user_id` int(11) NOT NULL,
  `action` varchar(255) NOT NULL,
  `log_time` timestamp NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `logs`
--

INSERT INTO `logs` (`log_id`, `user_id`, `action`, `log_time`) VALUES
(1, 1, 'Logged in', '2025-02-01 15:34:33'),
(2, 2, 'Updated HR policy', '2025-02-01 15:34:33'),
(3, 3, 'Uploaded financial report', '2025-02-01 15:34:33'),
(4, 4, 'Submitted ad design', '2025-02-01 15:34:33'),
(5, 5, 'Updated logistics strategy', '2025-02-01 15:34:33'),
(6, 6, 'Modified sales data', '2025-02-01 15:34:33'),
(7, 7, 'Developed chatbot prototype', '2025-02-01 15:34:33'),
(8, 8, 'Reviewed contract terms', '2025-02-01 15:34:33'),
(9, 9, 'Tested new product', '2025-02-01 15:34:33'),
(10, 10, 'Trained AI model', '2025-02-01 15:34:33'),
(11, 10, 'Changed task status to Completed', '2025-05-13 13:42:07'),
(12, 9, 'Changed task status to In Progress', '2025-05-15 16:19:12'),
(13, 9, 'Changed task status to Completed', '2025-05-15 16:22:32'),
(14, 9, 'Changed task status to In Progress', '2025-05-16 13:42:33');

-- --------------------------------------------------------

--
-- Table structure for table `projects`
--

CREATE TABLE `projects` (
  `project_id` int(11) NOT NULL,
  `project_name` varchar(100) NOT NULL,
  `start_date` date NOT NULL,
  `end_date` date NOT NULL,
  `department_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `projects`
--

INSERT INTO `projects` (`project_id`, `project_name`, `start_date`, `end_date`, `department_id`) VALUES
(1, 'Website Redesign', '2024-01-01', '2024-06-30', 1),
(2, 'HR System Upgrade', '2024-02-01', '2024-07-30', 2),
(3, 'Budget Planning', '2024-03-01', '2024-08-30', 3),
(4, 'Marketing Campaign', '2024-04-01', '2024-09-30', 4),
(5, 'Logistics Optimization', '2024-05-01', '2024-10-30', 5),
(6, 'Sales Expansion', '2024-06-01', '2024-11-30', 6),
(7, 'Customer Service AI', '2024-07-01', '2024-12-30', 7),
(8, 'Legal Compliance', '2024-08-01', '2025-01-30', 8),
(11, 'BU Voting Website', '2025-05-19', '2025-05-26', 1);

-- --------------------------------------------------------

--
-- Table structure for table `roles`
--

CREATE TABLE `roles` (
  `role_id` int(11) NOT NULL,
  `role_name` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `roles`
--

INSERT INTO `roles` (`role_id`, `role_name`) VALUES
(1, 'Admin'),
(5, 'Analyst'),
(4, 'Designer'),
(3, 'Developer'),
(7, 'Finance'),
(6, 'HR'),
(9, 'Intern'),
(2, 'Manager'),
(10, 'Sales'),
(8, 'Support');

-- --------------------------------------------------------

--
-- Table structure for table `tasks`
--

CREATE TABLE `tasks` (
  `task_id` int(11) NOT NULL,
  `task_name` varchar(255) NOT NULL,
  `assigned_to` int(11) NOT NULL,
  `project_id` int(11) NOT NULL,
  `status` enum('Pending','In Progress','Completed') DEFAULT 'Pending'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `tasks`
--

INSERT INTO `tasks` (`task_id`, `task_name`, `assigned_to`, `project_id`, `status`) VALUES
(1, 'Create Wireframes', 1, 1, 'Pending'),
(2, 'Database Migration', 2, 2, 'In Progress'),
(3, 'Financial Report', 3, 3, 'Completed'),
(4, 'Ad Design', 4, 4, 'Pending'),
(5, 'Route Planning', 5, 5, 'Pending'),
(6, 'Sales Strategy', 6, 6, 'In Progress'),
(7, 'Chatbot Development', 7, 7, 'Completed'),
(8, 'Contract Review', 8, 8, 'Pending'),
(9, 'Prototype Testing', 9, 9, 'Pending'),
(10, 'AI Model Training', 10, 10, 'Completed'),
(11, 'Prototyping', 9, 11, 'Pending'),
(13, 'Database Design', 9, 1, 'Completed'),
(14, 'Flowchart', 9, 11, 'In Progress');

--
-- Triggers `tasks`
--
DELIMITER $$
CREATE TRIGGER `trg_task_status_change` AFTER UPDATE ON `tasks` FOR EACH ROW BEGIN
  IF OLD.status != NEW.status THEN
    INSERT INTO logs(user_id, action)
    VALUES (NEW.assigned_to, CONCAT('Changed task status to ', NEW.status));
  END IF;
END
$$
DELIMITER ;

-- --------------------------------------------------------

--
-- Table structure for table `users`
--

CREATE TABLE `users` (
  `user_id` int(11) NOT NULL,
  `name` varchar(100) NOT NULL,
  `password` varchar(100) NOT NULL,
  `email` varchar(100) NOT NULL,
  `role_id` int(11) NOT NULL,
  `department_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `users`
--

INSERT INTO `users` (`user_id`, `name`, `password`, `email`, `role_id`, `department_id`) VALUES
(9, 'Neco Clemente', 'neco1234', 'necoclemente@gmail.com', 1, 7);

--
-- Indexes for dumped tables
--

--
-- Indexes for table `admin`
--
ALTER TABLE `admin`
  ADD PRIMARY KEY (`admin_id`);

--
-- Indexes for table `departments`
--
ALTER TABLE `departments`
  ADD PRIMARY KEY (`department_id`),
  ADD UNIQUE KEY `department_name` (`department_name`);

--
-- Indexes for table `files`
--
ALTER TABLE `files`
  ADD PRIMARY KEY (`file_id`),
  ADD KEY `uploaded_by` (`uploaded_by`),
  ADD KEY `task_id` (`task_id`);

--
-- Indexes for table `logs`
--
ALTER TABLE `logs`
  ADD PRIMARY KEY (`log_id`),
  ADD KEY `user_id` (`user_id`);

--
-- Indexes for table `projects`
--
ALTER TABLE `projects`
  ADD PRIMARY KEY (`project_id`),
  ADD KEY `department_id` (`department_id`);

--
-- Indexes for table `roles`
--
ALTER TABLE `roles`
  ADD PRIMARY KEY (`role_id`),
  ADD UNIQUE KEY `role_name` (`role_name`);

--
-- Indexes for table `tasks`
--
ALTER TABLE `tasks`
  ADD PRIMARY KEY (`task_id`),
  ADD KEY `assigned_to` (`assigned_to`),
  ADD KEY `project_id` (`project_id`);

--
-- Indexes for table `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`user_id`),
  ADD UNIQUE KEY `email` (`email`),
  ADD KEY `role_id` (`role_id`),
  ADD KEY `department_id` (`department_id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `admin`
--
ALTER TABLE `admin`
  MODIFY `admin_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT for table `departments`
--
ALTER TABLE `departments`
  MODIFY `department_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=12;

--
-- AUTO_INCREMENT for table `files`
--
ALTER TABLE `files`
  MODIFY `file_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;

--
-- AUTO_INCREMENT for table `logs`
--
ALTER TABLE `logs`
  MODIFY `log_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=15;

--
-- AUTO_INCREMENT for table `projects`
--
ALTER TABLE `projects`
  MODIFY `project_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=13;

--
-- AUTO_INCREMENT for table `roles`
--
ALTER TABLE `roles`
  MODIFY `role_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;

--
-- AUTO_INCREMENT for table `tasks`
--
ALTER TABLE `tasks`
  MODIFY `task_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=15;

--
-- AUTO_INCREMENT for table `users`
--
ALTER TABLE `users`
  MODIFY `user_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=15;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
