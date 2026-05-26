Unity Bus Simulator - Core Mechanics

A functional vehicle mechanics prototype built in **Unity** and **C#**. This project focuses on backend logic, trigger-based interactions, and basic resource management rather than perfect visuals. It serves as a practical implementation of fundamental game development systems.

Core Features & Mechanics

### 1. Vehicle Physics & Wheel System
* Configured realistic mass properties for a heavy transport vehicle.
* Custom wheel assignment system.
* Independent wheel mechanics: Rear wheels are responsible for drive rotation, while front wheels handle both rotation and steering logic.

### 2. Dynamic Fuel System
* Real-time fuel consumption logic implemented via C# scripts.
* The bus continuously consumes fuel based on its state (both while moving and idling).
* Fuel variables are exposed and can be monitored directly in the Unity Inspector.
* **Interactive Gas Station:** A trigger-based refueling zone that dynamically reads the bus's current fuel level and replenishes it.

### 3. Route & Checkpoint System
* Integrated a waypoint/checkpoint system along the road to guide the route progression.

### 4. Passenger (NPC) Logic via Triggers
* **Boarding Simulation:** At the first bus stop, NPCs with idle animations wait for the bus. Entering the bus stop checkpoint triggers an event that disables/destroys the NPCs, simulating passenger boarding.
* **Alighting Simulation:** At the final destination, previously hidden NPCs become visible once the bus enters the final trigger zone, simulating passengers getting off.
* *Note: The passenger system is built on functional trigger logic rather than complex visual animations, prioritizing code functionality.*

## 🛠️ Technical Details
* **Engine:** Unity
* **Language:** C#
* **Focus Area:** Scripts, Trigger Events (`OnTriggerEnter`), Physics logic.
