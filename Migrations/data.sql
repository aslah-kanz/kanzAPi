SET IDENTITY_INSERT Account.Principal ON
INSERT INTO Account.Principal (Id, Username, FirstName, LastName, Email, AcceptNewsLetter, Status, CreatedAt, CreatedBy, UpdatedAt, UpdatedBy) VALUES (0, 'system', 'System', 'Kanzway', 'system@kanzway.com', 0, 'Disabled', CURRENT_TIMESTAMP, 0, CURRENT_TIMESTAMP, 0)
INSERT INTO Account.Principal (Id, Username, Password, FirstName, LastName, Email, Type, AcceptNewsLetter, Status, CreatedAt, CreatedBy, UpdatedAt, UpdatedBy) VALUES (-1, 'admin', '$2a$13$nS/aYVm5fFyeKwIY6kcjXOfFQtzygUayP.BUmMKlkB4VVyBnXNdAa', 'Admin', 'Kanzway', 'admin@kanzway.com', 'Admin', 0, 'Active', CURRENT_TIMESTAMP, 0, CURRENT_TIMESTAMP, 0)
SET IDENTITY_INSERT Account.Principal OFF

SET IDENTITY_INSERT Account.Role ON
INSERT INTO Account.Role (Id, Name, CreatedAt, CreatedBy, UpdatedAt, UpdatedBy) VALUES (-1, 'Admin', CURRENT_TIMESTAMP, 0, CURRENT_TIMESTAMP, 0)
SET IDENTITY_INSERT Account.Role OFF

INSERT INTO Account.PrincipalRole (PrincipalId, RoleId) VALUES (-1, -1)

SET IDENTITY_INSERT Account.Privilege ON
INSERT INTO Account.Privilege (Id, Name, CreatedAt, CreatedBy, UpdatedAt, UpdatedBy) VALUES (1, 'RequestOtp', CURRENT_TIMESTAMP, 0, CURRENT_TIMESTAMP, 0)
INSERT INTO Account.Privilege (Id, Name, CreatedAt, CreatedBy, UpdatedAt, UpdatedBy) VALUES (2, 'GenerateToken', CURRENT_TIMESTAMP, 0, CURRENT_TIMESTAMP, 0)
INSERT INTO Account.Privilege (Id, Name, CreatedAt, CreatedBy, UpdatedAt, UpdatedBy) VALUES (11, 'AddPrincipal', CURRENT_TIMESTAMP, 0, CURRENT_TIMESTAMP, 0)
INSERT INTO Account.Privilege (Id, Name, CreatedAt, CreatedBy, UpdatedAt, UpdatedBy) VALUES (12, 'EditPrincipal', CURRENT_TIMESTAMP, 0, CURRENT_TIMESTAMP, 0)
INSERT INTO Account.Privilege (Id, Name, CreatedAt, CreatedBy, UpdatedAt, UpdatedBy) VALUES (13, 'RemovePrincipal', CURRENT_TIMESTAMP, 0, CURRENT_TIMESTAMP, 0)
INSERT INTO Account.Privilege (Id, Name, CreatedAt, CreatedBy, UpdatedAt, UpdatedBy) VALUES (14, 'ViewPrincipal', CURRENT_TIMESTAMP, 0, CURRENT_TIMESTAMP, 0)
INSERT INTO Account.Privilege (Id, Name, CreatedAt, CreatedBy, UpdatedAt, UpdatedBy) VALUES (21, 'CancelOrder', CURRENT_TIMESTAMP, 0, CURRENT_TIMESTAMP, 0)
INSERT INTO Account.Privilege (Id, Name, CreatedAt, CreatedBy, UpdatedAt, UpdatedBy) VALUES (22, 'CompleteOrder', CURRENT_TIMESTAMP, 0, CURRENT_TIMESTAMP, 0)
INSERT INTO Account.Privilege (Id, Name, CreatedAt, CreatedBy, UpdatedAt, UpdatedBy) VALUES (23, 'DeleteOrder', CURRENT_TIMESTAMP, 0, CURRENT_TIMESTAMP, 0)
INSERT INTO Account.Privilege (Id, Name, CreatedAt, CreatedBy, UpdatedAt, UpdatedBy) VALUES (24, 'ViewOrder', CURRENT_TIMESTAMP, 0, CURRENT_TIMESTAMP, 0)
SET IDENTITY_INSERT Account.Privilege OFF

INSERT INTO Account.RolePrivilege (RoleId, PrivilegeId) VALUES (-1, 11)
INSERT INTO Account.RolePrivilege (RoleId, PrivilegeId) VALUES (-1, 12)
INSERT INTO Account.RolePrivilege (RoleId, PrivilegeId) VALUES (-1, 13)
INSERT INTO Account.RolePrivilege (RoleId, PrivilegeId) VALUES (-1, 14)
