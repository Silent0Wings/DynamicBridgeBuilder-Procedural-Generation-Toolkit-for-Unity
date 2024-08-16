# DynamicBridgeBuilder: Procedural Generation Toolkit for Unity

## Overview

**DynamicBridgeBuilder** is a powerful toolkit for Unity that enables developers to create procedurally generated bridges in real-time. Whether you're developing an open-world game, an architectural visualization, or an educational tool, this system allows you to dynamically generate and customize bridges to fit any terrain or environment.

## Features

- **Physics-Based Interaction:** Leverage Unity's physics engine to create realistic, dynamic bridges that respond to forces and collisions.
- **Hinge Joints for Bridge Skeleton:** The system uses hinge joints to construct a flexible and interactive skeleton for each bridge, ensuring realistic movement and stability.
- **Procedural Generation:** Generate bridges that automatically adapt to their environment.
- **Dynamic Terrain Adaptation:** Bridges conform to various landscapes, ensuring perfect integration.
- **Customizable Bridge Sizes:** Create bridges of any size and width using base prefabs that are automatically scaled to fit your specifications.
- **Real-time Editing:** Adjust parameters in the Unity Editor with immediate visual feedback.
- **Optimized Performance:** Efficient algorithms ensure minimal impact on performance.

## Installation

1. Import the `.unitypackage` into your Unity project:
   - Go to `Assets` > `Import Package` > `Custom Package...`.
   - Select the `DynamicBridgeBuilder.unitypackage` and import it into your project.

2. Once imported, browse through the folders to locate the example scenes.

## Usage

### Experimenting with the Features

1. **Navigate to the Sample Scene:**
   - Browse through the following folders: `Suspension Bridge\RopeBridge-main\SampleScene`.
   - Open the `SampleScene.unity` file.

2. **Running the Test Scene:**
   - Once the `SampleScene` is open, press the `Play` button in Unity to run the scene.
   - Experiment with the various bridge types and features in real-time.

3. **Customizing the Bridge:**
   - Select the `BridgeGenerator` object in the scene to customize bridge parameters like length, width, and type.
   - Make adjustments and see how the bridge adapts in real-time within the test environment.

### Example Screenshots

Below are some example screenshots showcasing the features and capabilities of **DynamicBridgeBuilder**:

<p align="center">
  <img src= "https://github.com/Silent0Wings/DynamicBridgeBuilder-Procedural-Generation-Toolkit-for-Unity/blob/bd1195bb335901157c99602c1c0a660b85b0cdef/Screenshot/1.png" alt="Bridge Generation" width="250"/>
  <img src= "https://github.com/Silent0Wings/DynamicBridgeBuilder-Procedural-Generation-Toolkit-for-Unity/blob/bd1195bb335901157c99602c1c0a660b85b0cdef/Screenshot/2.png" alt="Bridge Generation" width="250"/>
  <img src= "https://github.com/Silent0Wings/DynamicBridgeBuilder-Procedural-Generation-Toolkit-for-Unity/blob/bd1195bb335901157c99602c1c0a660b85b0cdef/Screenshot/3.png" alt="Bridge Generation" width="250"/>
  <img src= "https://github.com/Silent0Wings/DynamicBridgeBuilder-Procedural-Generation-Toolkit-for-Unity/blob/bd1195bb335901157c99602c1c0a660b85b0cdef/Screenshot/4.png" alt="Bridge Generation" width="250"/>
  <img src= "https://github.com/Silent0Wings/DynamicBridgeBuilder-Procedural-Generation-Toolkit-for-Unity/blob/bd1195bb335901157c99602c1c0a660b85b0cdef/Screenshot/5.png" alt="Bridge Generation" width="250"/>
  <img src= "https://github.com/Silent0Wings/DynamicBridgeBuilder-Procedural-Generation-Toolkit-for-Unity/blob/bd1195bb335901157c99602c1c0a660b85b0cdef/Screenshot/6.png" alt="Bridge Generation" width="250"/>
  <img src= "https://github.com/Silent0Wings/DynamicBridgeBuilder-Procedural-Generation-Toolkit-for-Unity/blob/bd1195bb335901157c99602c1c0a660b85b0cdef/Screenshot/7.png" alt="Bridge Generation" width="250"/>
;
</p>

<p align="center">
  <img src="images/terrain_adaptation.png" alt="Terrain Adaptation" width="250"/>
  <img src="images/custom_bridge_types.png" alt="Customizable Bridge Types" width="250"/>
  <img src="images/real_time_editing.png" alt="Real-time Editing" width="250"/>
</p>

### Advanced Features

- **Custom Bridge Types:**
  - Extend the system by adding your custom bridge styles. Use the provided scripts as a base and modify the generation logic to fit your requirements.
  
- **Integration with Terrain:**
  - Ensure your bridge seamlessly fits into the environment by configuring terrain detection settings. The generator will adjust the bridge’s foundation and structure accordingly.

## Contributing

We welcome contributions to enhance DynamicBridgeBuilder! If you have ideas, suggestions, or improvements, feel free to fork the repository and submit a pull request.


