# PEC4

PEC4 para la asignatura de Programación 2D. Juego propio de vista top-down. Shooter de temática zombi.

## Introducción

Un juego de plataformas. Correspondiente a la práctica PC2 de la asignatura de programación 2D del master "Diseño y programación de videojuegos".
Está programado en **Unity 2020.3.18f1**, con repositorio en **GitLab** y control de versiones por **Sourcetree**.

El juego consiste en un juego de aventuras inspirado en **Super Mario Bros**. Concretamente en el nivel 1-1 de dicho video juego. El jugador tendrá que llegar al final del nivel para obtener la victoria.
Podrá recoger items para mejorar su puntuación, podrá derrotar enemigos saltando sobre ellos y podrá desbloquear el estado **Super** para poder recoger aun más elementos del juego y tener más resistencia contra enemigos.
dy a pro? Just edit this README.md and make it your own. Want to make it easy? [Use the template at the bottom](#editing-this-readme)!

## Enlaces del juego

YouTube: https://youtu.be/fixtAjOeKMs

GitLab: https://gitlab.com/pmolinapa/pec4/-/tree/Corregir/

## Controles

Teclado:
- movimiento con las teclas WASD y flechas.
- H para curar, si tenemos botiquines disponibles y si no estamos con la vida llena.
- R para recargar.

Ratón:
- Desplazamiento de la cámara si lo acercamos al borde de la pantalla.
- Zoom con la rueda del ratón.

## Mecánicas

- Recolección de recursos: cargadores y botiquines.
- Disparo para eliminar enemigos
- Sistema de puntuación.
- Salidas del mapa

## Jugador

- Maquina de estados con los estados:
-**Idle**: El jugador estará quieto.
-**Move**: El jugador se moverá
-**Reload**: Recargando el arma.
-**ReloadMove**: Moviendose mientras recarga el arma.
-**Dead**: Cuando muera.

El jugador tendrá que administrar los consumibles que podrá ir recogiendo. Tendrá un máximo de cargadores y botiquines.

## Consumibles

-**Cargadores**: Cada cargador le proporcionará 30 balas, si recarga con el cargador actual a medias, perderá las balas restantes.
-**Botiquines**: Un uso proporcionará hasta 40 puntos de salud.

Mientras se está recargando no se puede curar ni disparar y mientras se está curando no podrá recargar ni disparar. 

## Enemigos

-**Zombie1**:
Controlado por una maquina de estados con los estados:
-**Idle**: El zombie permanecerá en reposo.
-**Move** El zombie estará en movimiento hacia el objetivo, ya sea una posición aleatoria o el jugador.
-**Turn**: En este estado buscará una posicion aleatoria para vagabundear.
-**PlayerDetected**: Si ha detectado el jugador.
-**Attack**: Si está en rango de ataque, atacará al jugador.
-**Dead**: Cuando muera.

## Fin de mapa

Las salidas nos permitirán llegar al siguiente nivel. Una vez nos acercamos, pasarán unos segundos hasta que podamos pasar al siguiente nivel.
Esto hace peligroso correr hasta la salida directamente.

## Creditos:

De https://opengameart.org/ 

**Sprites**
Zombie: rileygombart
Jugador: rileygombar
Sangre: PWL
Puntero: fluxord
HUD: chabull
AK-47 HUD: PixelMannen
Botiquín: knik1985
Efecto curación: knik1985
AmmoBox: NiceGraphic

**Sonidos**
Pasos: congusbongus
Victoria: Listener
Curación: Zoltan Mihalyi
Recogida de botiquín: Zoltan Mihalyi
Muerte jugador: congusbongus

**Música**
Level: 1-1 Human Antagonist
Level: 1-2 Human Antagonist
MainMenu: The Real Monoton Artist
GameOver: zuvizu
Victoria: Fupi
