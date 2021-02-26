# ML-Agents-Maze-Rat

## Usage

if you want to train this agent, you need to have installed [ML-Agents.](https://github.com/Unity-Technologies/ml-agents)

I followed this tutorial: [How to use Machine Learning AI in Unity! (ML-Agents).](https://www.youtube.com/watch?v=zPFU30tbyKs)

Made in Unity 2020.2.4f.

Alpha 1: TimeScale = 1
Alpha 2: TimeScale = 5
Alpha 3: TimeScale = 10
...
Alpha 9: TimeScale = 45
Alpha 0: TimeScale *= 2


## Notes

01-10 Entrenamiento izquierda-Arriba </br>
11 Arriba con un poco de derecha</br>
13 Objetivos Abajo, Arria casi completado. Empieza a completar el que está detrás de la pared.</br>
14 Cambiar un Inicio a la contra esquina.</br>
14.5 Cambiar varios Inicio de posición. Uno está a la media vuelta completando. Derecha no lo está logrando.</br>
15 Cambio en un laberinto.</br>
16 Laberinto rotado de 2x2 - Inicio y Final no se mueven.</br>
17 laberinto rotado y aleatorio 2x2 - Inicio y Final no se mueven.</br>

16->18 Laberinto aleatorio 2x2 - Inicio y Final  aleatorios. EasyEnd activado en Generator.</br>
16->19  Laberinto aleatorio 2x2 - Inicio y Final  aleatorios.</br>

MazeAgent_003 Ray Sensor Perception 3D component. Laberinto aleatorio 3x2 - Inicio y Final aleatorios.</br>
MazeAgent_004 Ray Sensor Perception 3D component, modificación de parámetros.Agrega rotation del agente. Laberinto aleatorio 3x2 - Inicio y Final aleatorios.</br>
MazeAgent_005 6,000,000 pasos. Penalti por cada paso que pase de 1/MaxStep; MaxStep == 10,000, si StepCount ==MaxStep entonces Reward = -1</br>
MazeAgent_005 -> MazeAgent_006 Estaba entrenando hacia un objetivo que no era.</br>
MazeAgent_007 Desde cero con nuevos parámetros.</br>

015 Nuevo, stacked 3 en raySensor</br>
016 Cambios en Rewards al chocar con un muro y en los maxSteps a 3000</br>

MazeAgent-06 Laberinto aleatorio, sin penalización por chocar con pared. Penalización por existir.</br>
MazeAgent-07 Laberinto aleatorio, penalización por chocar con pared. Penalización por existir.</br>

-10 modificación a los parámetros del archivo de configuración, 1000000 steps. Por agente maxSteps: 3000</br>
-10 -> -11 Completa el laberinto 2x2, Inicio y Final estáticos.</br>
-11 -> -12 Completa laberinto 2x2 aleatorio.</br>
-12 -> -13 Laberinto aleatorio 3x2, algunos agentes lo resuelven otros fallan, hay 4 agentes con 2x2. Al finalizar la mayoría lo completa pero aún hay alguna que otra falla.</br>
-13 -> -14 Un laberinto aleatorio de 5x5, dos de 2x2, diez de 3x4, diez de 4x3, cinco de 3x5 y cinco de 5x3.</br>
    &nbsp;&nbsp;&nbsp;Muchos fallos al inicio, pero uno que otro logra completarlos.</br>
    &nbsp;&nbsp;&nbsp;Fallos por tiempo, tal vez aumentar el número máximo de pasos por agente.</br>
    &nbsp;&nbsp;&nbsp;168000 pasos, 5x5 no completado, choque con pared. Todos los demás al menos una vez han sido completodos.</br>
    &nbsp;&nbsp;&nbsp;324000 pasos, las recompensas empezaron a bajar</br>

-14 -> -15 maxSteps: 5000, desactivado el laberinto 5x5</br>
-15 -> -16 8000000 steps</br>
-16 -> -17 3x4 y 4x3</br>




## Credits
The Maze generator algorithm is from [Perfect-Maze-Generator-Unity3d](https://github.com/orifmilod/Perfect-Maze-Generator-Unity3d) with modifications to meet my needs.

The Assets/SharedAssets folders is from the ML-Agents toolkit.

## License
[MIT](https://choosealicense.com/licenses/mit/)
