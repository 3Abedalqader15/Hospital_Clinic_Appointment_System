-- ?????: ???? ???????? ???????
DELETE FROM TimeSlots;
DELETE FROM Appointments;
DELETE FROM Doctors;
DELETE FROM Patients;
DELETE FROM UserRoles;

-- ??????: ??? UserRoles (???? ?? ?? ?????)
INSERT INTO UserRoles (User_Id, Role_Id) VALUES
(1000, 3),  -- Abood321 - Admin
(1001, 1),  -- Dr.Ahmad Ali - Doctor
(1002, 1),  -- Dr.Ali Ahmad - Doctor
(1026, 1),  -- Dr.Abdalhkeem - Doctor
(1028, 1),  -- Dr.Abood - Doctor
(1029, 2),  -- Yazan Qasem - Patient
(1030, 2),  -- Hala Mustafa - Patient
(1031, 2),  -- Fadi Shaker - Patient
(1032, 2),  -- Laila Bitar - Patient
(1033, 2),  -- Ziad Harb - Patient
(1034, 2),  -- Reem Saleh - Patient
(1035, 2),  -- Bassam Najjar - Patient
(1036, 2),  -- Hiba Zain - Patient
(1037, 2),  -- Nabil Srour - Patient
(1038, 2);  -- Dalia Awad - Patient

-- ??????: ??? ???????? (4 ??? - ?? ?? ?????)
INSERT INTO Doctors (User_Id, Specialization, LicenseNumber, ExperienceYears, Bio, profilePictureUrl, isActive, Created_At, Name)
VALUES
(1001, 'Cardiology', 'DOC-001', 10, 'Cardiac surgeon specialist', '/images/doctor1.jpg', 1, '2026-01-30', 'Dr.Ahmad Ali'),
(1002, 'Orthopedics', 'DOC-002', 8, 'Orthopedic specialist', '/images/doctor2.jpg', 1, '2026-01-30', 'Dr.Ali Ahmad'),
(1026, 'General Medicine', 'DOC-003', 5, 'General practitioner', '/images/doctor3.jpg', 1, '2026-02-02', 'Dr.Abdalhkeem'),
(1028, 'Pediatrics', 'DOC-004', 7, 'Children specialist', '/images/doctor4.jpg', 1, '2026-02-02', 'Dr.Abood');

-- ??????: ??? ??????
INSERT INTO Patients (User_Id, MedicalHistory, EmergencyNumber, isActive, Name)
VALUES
(1029, 'No major issues', '0779875110', 1, 'Yazan Qasem'),
(1030, 'Asthma', '0779875111', 1, 'Hala Mustafa'),
(1031, 'Allergies', '0777340612', 1, 'Fadi Shaker'),
(1032, 'Migraine', '0779875112', 1, 'Laila Bitar'),
(1033, 'Back pain', '0777340613', 1, 'Ziad Harb'),
(1034, 'No major issues', '0779875113', 1, 'Reem Saleh'),
(1035, 'Heart condition', '0777340614', 1, 'Bassam Najjar'),
(1036, 'No major issues', '0779875114', 1, 'Hiba Zain'),
(1037, 'Diabetes', '0777340615', 1, 'Nabil Srour'),
(1038, 'No major issues', '0779875115', 1, 'Dalia Awad');

-- ??????: TimeSlots - ?????? ????? ??? ????? ??????
-- 0=Sunday, 1=Monday, 2=Tuesday, 3=Wednesday, 4=Thursday, 5=Friday, 6=Saturday
INSERT INTO TimeSlots (DoctorId, DayOfWeek, StartTime, EndTime, SlotDuration, isActive)
VALUES
-- Dr.Ahmad Ali (Id=1) - ???? ????? ?????????
(20, 'Sunday', '09:00:00', '12:00:00', 30, 1),
(20, 'Tuesday', '09:00:00', '12:00:00', 30, 1),
(20, 'Thursday', '14:00:00', '17:00:00', 30, 1),

-- Dr.Ali Ahmad (Id=2) - ???? ??????? ?????????
(21, 'Monday', '10:00:00', '13:00:00', 30, 1),
(21, 'Wednesday', '10:00:00', '13:00:00', 30, 1),
(21, 'Thursday', '09:00:00', '12:00:00', 30, 1),

-- Dr.Abdalhkeem (Id=3) - ???? ????? ????????? ???????
(22, 'Sunday', '14:00:00', '18:00:00', 45, 1),
(22, 'Tuesday', '14:00:00', '18:00:00', 45, 1),
(22, 'Thursday', '10:00:00', '14:00:00', 45, 1),

-- Dr.Abood (Id=4) - ???? ??????? ????????? ???????
(23, 'Monday', '15:00:00', '19:00:00', 60, 1),
(23, 'Wednesday', '15:00:00', '19:00:00', 60, 1),
(23, 'Friday', '09:00:00', '13:00:00', 60, 1);

-- ??????: Appointments
INSERT INTO Appointments (DoctorId, PatientId, AppointmentDate, Reason, Status, Notes, ReminderSent, CreatedAt, UpdatedAt)
VALUES
(20, 23, '2026-02-09 09:00:00', 'Regular checkup', 'Scheduled', 'First visit', 0, GETDATE(), GETDATE()),
(20, 24, '2026-02-09 09:30:00', 'Heart consultation', 'Scheduled', 'Follow-up', 0, GETDATE(), GETDATE()),
(21, 25, '2026-02-10 10:00:00', 'Knee pain', 'Scheduled', 'X-ray required', 0, GETDATE(), GETDATE()),
(21, 26, '2026-02-10 10:30:00', 'Back pain', 'Scheduled', 'xxxxxx', 0, GETDATE(), GETDATE()),
(22, 27, '2026-02-09 14:00:00', 'General checkup', 'Scheduled', 'xxxxx', 0, GETDATE(), GETDATE()),
(22, 28, '2026-02-09 14:45:00', 'Fever', 'Scheduled', 'xxxxx', 0, GETDATE(), GETDATE()),
(23, 29, '2026-02-10 15:00:00', 'Vaccination', 'Scheduled', '6 months', 0, GETDATE(), GETDATE()),
(23, 30, '2026-02-10 16:00:00', 'Growth check', 'Scheduled', 'xxxxx', 0, GETDATE(), GETDATE());

-- ???? ?? ???????
SELECT 'Doctors' as [Table], COUNT(*) as Count FROM Doctors
UNION ALL SELECT 'Patients', COUNT(*) FROM Patients
UNION ALL SELECT 'TimeSlots', COUNT(*) FROM TimeSlots
UNION ALL SELECT 'Appointments', COUNT(*) FROM Appointments;