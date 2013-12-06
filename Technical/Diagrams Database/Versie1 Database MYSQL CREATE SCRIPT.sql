SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='TRADITIONAL,ALLOW_INVALID_DATES';

CREATE SCHEMA IF NOT EXISTS `mydb` DEFAULT CHARACTER SET utf8 COLLATE utf8_general_ci ;
USE `mydb` ;

-- -----------------------------------------------------
-- Table `mydb`.`rol`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `mydb`.`rol` ;

CREATE TABLE IF NOT EXISTS `mydb`.`rol` (
  `name` VARCHAR(10) NOT NULL,
  PRIMARY KEY (`name`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`user`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `mydb`.`user` ;

CREATE TABLE IF NOT EXISTS `mydb`.`user` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `rol_name` VARCHAR(10) NOT NULL,
  `student_number` INT NULL,
  `firstname` VARCHAR(45) NULL,
  `lastname` VARCHAR(45) NULL,
  `profile_image` VARCHAR(255) NULL,
  `email` VARCHAR(45) NULL,
  PRIMARY KEY (`id`),
  INDEX `fk_user_rol_idx` (`rol_name` ASC),
  CONSTRAINT `fk_user_rol`
    FOREIGN KEY (`rol_name`)
    REFERENCES `mydb`.`rol` (`name`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`baro_templates`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `mydb`.`baro_templates` ;

CREATE TABLE IF NOT EXISTS `mydb`.`baro_templates` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `template_name` VARCHAR(45) NULL,
  `rating_type` VARCHAR(20) NULL,
  `anonymous` TINYINT NULL,
  PRIMARY KEY (`id`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`project`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `mydb`.`project` ;

CREATE TABLE IF NOT EXISTS `mydb`.`project` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `baro_template_id` INT NOT NULL,
  `name` VARCHAR(45) NULL,
  `description` VARCHAR(45) NULL,
  `blok` VARCHAR(45) NULL,
  `start_date` DATE NULL,
  `end_date` DATE NULL,
  PRIMARY KEY (`id`),
  INDEX `fk_project_project_barometers1_idx` (`baro_template_id` ASC),
  CONSTRAINT `fk_project_project_barometers1`
    FOREIGN KEY (`baro_template_id`)
    REFERENCES `mydb`.`baro_templates` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`project_groups`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `mydb`.`project_groups` ;

CREATE TABLE IF NOT EXISTS `mydb`.`project_groups` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `user_id` INT NOT NULL,
  PRIMARY KEY (`id`),
  INDEX `fk_project_groups_user1_idx` (`user_id` ASC),
  CONSTRAINT `fk_project_groups_user1`
    FOREIGN KEY (`user_id`)
    REFERENCES `mydb`.`user` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`tutor`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `mydb`.`tutor` ;

CREATE TABLE IF NOT EXISTS `mydb`.`tutor` (
)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`mentor`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `mydb`.`mentor` ;

CREATE TABLE IF NOT EXISTS `mydb`.`mentor` (
)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`project_group`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `mydb`.`project_group` ;

CREATE TABLE IF NOT EXISTS `mydb`.`project_group` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `project_id` INT NOT NULL,
  `tutor_user_id` INT NULL,
  `group_code` VARCHAR(45) NULL,
  `group_end_grade` INT NULL,
  PRIMARY KEY (`id`),
  INDEX `fk_project_group_project1_idx` (`project_id` ASC),
  INDEX `fk_project_group_user1_idx` (`tutor_user_id` ASC),
  CONSTRAINT `fk_project_group_project1`
    FOREIGN KEY (`project_id`)
    REFERENCES `mydb`.`project` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_project_group_user1`
    FOREIGN KEY (`tutor_user_id`)
    REFERENCES `mydb`.`user` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`project_group_members`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `mydb`.`project_group_members` ;

CREATE TABLE IF NOT EXISTS `mydb`.`project_group_members` (
  `id` INT NOT NULL,
  `project_group_id` INT NOT NULL,
  `student_user_id` INT NOT NULL,
  `end_grade` INT NULL,
  PRIMARY KEY (`id`),
  INDEX `fk_project_group_members_project_group1_idx` (`project_group_id` ASC),
  INDEX `fk_project_group_members_user1_idx` (`student_user_id` ASC),
  CONSTRAINT `fk_project_group_members_project_group1`
    FOREIGN KEY (`project_group_id`)
    REFERENCES `mydb`.`project_group` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_project_group_members_user1`
    FOREIGN KEY (`student_user_id`)
    REFERENCES `mydb`.`user` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`project_reportdates`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `mydb`.`project_reportdates` ;

CREATE TABLE IF NOT EXISTS `mydb`.`project_reportdates` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `project_id` INT NOT NULL,
  `is_endreport` TINYINT NULL,
  `week_label` VARCHAR(30) NULL,
  `start_date` DATETIME NULL,
  `end_date` DATETIME NULL,
  INDEX `fk_project_reportdates_project1_idx` (`project_id` ASC),
  PRIMARY KEY (`id`),
  CONSTRAINT `fk_project_reportdates_project1`
    FOREIGN KEY (`project_id`)
    REFERENCES `mydb`.`project` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`baro_aspects`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `mydb`.`baro_aspects` ;

CREATE TABLE IF NOT EXISTS `mydb`.`baro_aspects` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `baro_templates_id` INT NOT NULL,
  `parent_id` INT NULL,
  `is_head_aspect` TINYINT NULL,
  `can_be_filled` TINYINT NULL,
  `name` VARCHAR(45) NULL,
  `description` VARCHAR(45) NULL,
  PRIMARY KEY (`id`),
  INDEX `fk_baro_aspects_baro_aspects1_idx` (`parent_id` ASC),
  INDEX `fk_baro_aspects_baro_templates1_idx` (`baro_templates_id` ASC),
  CONSTRAINT `fk_baro_aspects_baro_aspects1`
    FOREIGN KEY (`parent_id`)
    REFERENCES `mydb`.`baro_aspects` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_baro_aspects_baro_templates1`
    FOREIGN KEY (`baro_templates_id`)
    REFERENCES `mydb`.`baro_templates` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`reports`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `mydb`.`reports` ;

CREATE TABLE IF NOT EXISTS `mydb`.`reports` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `reporter_id` INT NOT NULL,
  `subject_id` INT NOT NULL,
  `project_reportdates_id` INT NOT NULL,
  `baro_aspects_id` INT NOT NULL,
  `grade` INT NULL DEFAULT 0,
  `comment` TEXT NULL,
  PRIMARY KEY (`id`),
  INDEX `fk_reports_user1_idx` (`subject_id` ASC),
  INDEX `fk_reports_user2_idx` (`reporter_id` ASC),
  INDEX `fk_reports_project_reportdates1_idx` (`project_reportdates_id` ASC),
  INDEX `fk_reports_baro_aspects1_idx` (`baro_aspects_id` ASC),
  CONSTRAINT `fk_reports_user1`
    FOREIGN KEY (`subject_id`)
    REFERENCES `mydb`.`user` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_reports_user2`
    FOREIGN KEY (`reporter_id`)
    REFERENCES `mydb`.`user` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_reports_project_reportdates1`
    FOREIGN KEY (`project_reportdates_id`)
    REFERENCES `mydb`.`project_reportdates` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_reports_baro_aspects1`
    FOREIGN KEY (`baro_aspects_id`)
    REFERENCES `mydb`.`baro_aspects` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`project_owners`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `mydb`.`project_owners` ;

CREATE TABLE IF NOT EXISTS `mydb`.`project_owners` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `user_id` INT NOT NULL,
  `project_id` INT NOT NULL,
  PRIMARY KEY (`id`),
  INDEX `fk_user_has_project_project1_idx` (`project_id` ASC),
  INDEX `fk_user_has_project_user1_idx` (`user_id` ASC),
  CONSTRAINT `fk_user_has_project_user1`
    FOREIGN KEY (`user_id`)
    REFERENCES `mydb`.`user` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_user_has_project_project1`
    FOREIGN KEY (`project_id`)
    REFERENCES `mydb`.`project` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
