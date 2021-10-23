CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name varchar(255) COMMENT 'User Name',
  email varchar(255) COMMENT 'User Email',
  picture varchar(255) COMMENT 'User Picture'
) default charset utf8 COMMENT '';

CREATE TABLE IF NOT EXISTS comments(
  id int NOT NULL PRIMARY KEY AUTO_INCREMENT comment 'primary key',
  creatorId varchar(255) NOT NULL comment 'creatorId',
  body varchar(5000) NOT NULL comment 'comments body',
  blog int NOT NULL comment 'id of blog',
  FOREIGN KEY(creatorId) REFERENCES accounts(id) ON DELETE CASCADE,
  FOREIGN KEY(blog) REFERENCES blogs(id) ON DELETE CASCADE 
) default charset utf8 comment '';
