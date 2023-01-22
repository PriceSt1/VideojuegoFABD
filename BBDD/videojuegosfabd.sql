-- phpMyAdmin SQL Dump
-- version 5.2.0
-- https://www.phpmyadmin.net/
--
-- Servidor: 127.0.0.1
-- Tiempo de generación: 17-01-2023 a las 14:09:16
-- Versión del servidor: 10.4.25-MariaDB
-- Versión de PHP: 8.1.10

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de datos: `videojuegosfabd`
--
DROP DATABASE IF EXISTS videojuegosfabd;
CREATE DATABASE videojuegosfabd;
USE videojuegosfabd;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `tcliente`
--

CREATE TABLE `tcliente` (
  `codcliente` varchar(6) DEFAULT NULL,
  `nombre` varchar(40) DEFAULT NULL,
  `apellido` varchar(40) DEFAULT NULL,
  `dni` varchar(9) DEFAULT NULL,
  `direccion` varchar(60) DEFAULT NULL,
  `email` varchar(40) DEFAULT NULL,
  `borrado` varchar(1) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `tfactura`
--

CREATE TABLE `tfactura` (
  `codfactura` varchar(6) DEFAULT NULL,
  `cliente` varchar(50) DEFAULT NULL,
  `fechafactura` varchar(60) DEFAULT NULL,
  `borrado` varchar(1) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `tgenero`
--

CREATE TABLE `tgenero` (
  `genero` varchar(60) DEFAULT NULL,
  `borrado` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Volcado de datos para la tabla `tgenero`
--

INSERT INTO `tgenero` (`genero`, `borrado`) VALUES
('Shooter', 0),
('RPG', 0),
('RTS', 0),
('Fantasy', 0),
('Indie', 0),
('MMORPG', 0),
('Accion', 0),
('Simulacion', 0),
('Espacio', 0);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `tlinea`
--

CREATE TABLE `tlinea` (
  `codfactura` varchar(6) DEFAULT NULL,
  `videojuego` varchar(60) DEFAULT NULL,
  `cantidad` varchar(250) DEFAULT NULL,
  `total` varchar(250) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `tlineafactura`
--

CREATE TABLE `tlineafactura` (
  `codfactura` varchar(6) DEFAULT NULL,
  `vidoejuego` varchar(250) DEFAULT NULL,
  `cantidad` varchar(250) DEFAULT NULL,
  `total` varchar(250) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `tvideojuego`
--

CREATE TABLE `tvideojuego` (
  `codvideojuego` varchar(6) DEFAULT NULL,
  `titulo` varchar (40) DEFAULT NULL,
  `desarrollador` varchar(40) DEFAULT NULL,
  `editor` varchar(40) DEFAULT NULL,
  `genero` varchar(30) DEFAULT NULL,
  `precio` varchar(30) DEFAULT NULL,
  `fechalanzamiento` varchar(30) DEFAULT NULL,
  `idioma` varchar(30) DEFAULT NULL,
  `pegi` varchar(2) DEFAULT NULL,
  `portada` TEXT DEFAULT NULL,
  `trailer` TEXT DEFAULT NULL,
  `borrado` varchar(1) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
