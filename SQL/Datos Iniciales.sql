-- CATEGORÍAS
INSERT INTO [dbo].[Categoria] (descripcion, ruta_imagen, activo) VALUES
('Maquillaje Facial', NULL, 1),
('Cuidado de la Piel', NULL, 1),
('Maquillaje de Ojos', NULL, 1),
('Labios', NULL, 1),
('Uñas', NULL, 1);
GO

-- PRODUCTOS BASE
INSERT INTO [dbo].[Producto] (id_categoria, descripcion, detalle, precio, existencias, ruta_imagen, activo) VALUES
(1, 'Base Líquida HD', 'Base de alta cobertura con acabado natural. Disponible en varios tonos.', 16900.0, 50, NULL, 1),
(1, 'Polvo Compacto Mate', 'Polvo facial para sellar maquillaje y controlar brillo.', 9500.0, 40, NULL, 1),
(2, 'Crema Hidratante Facial', 'Hidratante ligera para uso diario, ideal para pieles sensibles.', 12500.0, 30, NULL, 1),
(2, 'Mascarilla Detox de Carbón', 'Limpieza profunda para piel grasa. Ayuda a reducir puntos negros.', 11200.0, 20, NULL, 1),
(3, 'Paleta de Sombras Nude', '12 tonos neutros mate y brillantes para cualquier ocasión.', 23900.0, 25, NULL, 1),
(3, 'Delineador Líquido Waterproof', 'Color negro intenso de larga duración, punta precisa.', 7900.0, 60, NULL, 1),
(4, 'Labial Mate Rojo Clásico', 'Labial líquido con acabado mate y fórmula de larga duración.', 8700.0, 35, NULL, 1),
(4, 'Brillo Labial Transparente', 'Gloss hidratante con acabado brillante y suave aroma a vainilla.', 6800.0, 50, NULL, 1),
(5, 'Esmalte Color Coral', 'Esmalte con acabado gel, secado rápido y larga duración.', 5600.0, 45, NULL, 1),
(5, 'Removedor de Esmalte Sin Acetona', 'Remueve esmalte sin resecar ni debilitar las uñas.', 5200.0, 30, NULL, 1);
GO

-- PRODUCTOS CON IMAGEN
INSERT INTO [dbo].[Producto] (id_categoria, descripcion, detalle, precio, existencias, ruta_imagen, activo) VALUES
(2, 'Limpiador Facial', 'Limpia profundamente el rostro sin resecar la piel.', 8500.0, 20, 'https://m.media-amazon.com/images/I/41W0vsHvlrL.jpg', 1),
(2, 'Tónico Refrescante', 'Tonifica y prepara la piel para los siguientes pasos.', 6200.0, 15, 'https://m.media-amazon.com/images/I/616Boda2iTL._UF894,1000_QL80_.jpg', 1),
(2, 'Sérum de Vitamina C', 'Aclara la piel y reduce manchas.', 10500.0, 10, 'https://www.nipandfab.com/cdn/shop/products/VITC_SERUM.jpg?v=1680019460', 1),
(2, 'Crema Hidratante', 'Hidratación intensa y duradera.', 9500.0, 25, 'https://www.nipandfab.com/cdn/shop/files/Packshot-Hyaluronichybridgelcream_2.jpg?v=1693824989', 1),
(2, 'Protector Solar FPS50', 'Protege la piel del sol sin dejar residuo.', 8900.0, 30, 'https://eu.nipandfab.com/cdn/shop/products/SKSPFGLOW_1.jpg?v=1655411857', 1);
GO

-- RUTINAS
INSERT INTO [dbo].[Rutina] (IdRutina, Nombre, Descripcion, Imagen) VALUES
(1, 'Rutina de mañana', 'Cuida tu piel en las mañanas con estos productos', 'https://www.nipandfab.com/cdn/shop/files/Packshot_-_ageless-radiance-morning-skincare-routine-kit-for-normal-mature-skin_18.jpg?v=1752497381&width=763'),
(2, 'Rutina de noche', 'Repara tu piel en la noche antes de dormir', 'https://www.nipandfab.com/cdn/shop/files/Retinol_Fix_regime_kit_3295baba-a8f2-4b51-9f7c-1599d4541f9d.jpg?v=1752498198&width=763'),
(3, 'Rutina de maquillaje natural', 'Un look fresco y natural para todos los días', 'https://amparolopez.mx/cdn/shop/files/Verpackungen.jpg?v=1635967453&width=1260'),
(4, 'Rutina post-gym', 'Limpia y refresca tu rostro después del ejercicio', 'https://apresbeauty.co/cdn/shop/files/Apres_ProductsOnly4031.webp?v=1730102338'),
(5, 'Rutina de spa en casa', 'Relájate y consiente tu piel con esta rutina de spa sin salir de casa.', 'https://www.gosupps.com/media/catalog/product/7/1/71GM8JYAIfL.jpg'),
(6, 'Rutina piel radiante', 'Devuélvele a tu piel su brillo natural con esta rutina de productos iluminadores.', 'https://ak.uecdn.es/p/112/thumbnail/entry_id/0_drk9cyes/width/660/cache_st/1546442139/type/2/bgcolor/000000/0_drk9cyes.jpg');
GO

-- ASOCIAR PRODUCTOS CON RUTINA DE MAÑANA (IdRutina = 1)
INSERT INTO [dbo].[RutinaProducto] (IdRutina, id_producto) VALUES
(1, 1),
(1, 2),
(1, 3),
(1, 4),
(1, 5);
GO

-- Team Members
INSERT INTO TeamMember (Nombre, Puesto, ImagenUrl, FacebookUrl, TwitterUrl, LinkedinUrl)
VALUES
('IBG', 'Fundador', '/img/equipo/ibg.jpg', 'https://facebook.com/ibg', 'https://twitter.com/ibg', 'https://linkedin.com/in/ibg'),
('Marcela Botero', 'Mercadeo y Ventas', '/img/equipo/marcela-botero.jpg', 'https://facebook.com/marcela', 'https://twitter.com/marcela', 'https://linkedin.com/in/marcela'),
('Teresa Duque', 'Atención al cliente', '/img/equipo/teresa-duque.jpg', 'https://facebook.com/teresa', 'https://twitter.com/teresa', 'https://linkedin.com/in/teresa');
GO

UPDATE TeamMember
SET PhotoPath = '/img/team/ibg.jpg'
WHERE FullName = 'IBG';

UPDATE TeamMember
SET PhotoPath = '/img/team/marcela.jpg'
WHERE FullName = 'MARCELA BOTERO';

UPDATE TeamMember
SET PhotoPath = '/img/team/teresa.jpg'
WHERE FullName = 'TERESA DUQUE';