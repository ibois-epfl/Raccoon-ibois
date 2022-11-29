Author: Petras Vestartas

This is a tool for CNC fabrication for drilling, milling, cutting, slicing, engraving and saw-blade fabrication. The tool-path was tested on machines: Orthogonal 5-axis Maka, Cardan 5-axis Maka and industrial robot arm ABB IRB 6400R. The application employs the G-Code using A and B rotations and XYZ values for translation. The algorithm considers machine methods such as the tool-changer, zero referencing, speed of movement, etc. The tool path can be simulated including the collision decetion  between timber, spindle and the table.

# Install
Download latest build from the release directory of github.

To run CNC G-Code download the Build folder and place in Grasshopper Libraries folder.

Important: add all tools parameters from the current CNC machine to Tools.txt file.


# Interface
For security reasons all tools are defined in Tools.txt file.

In order to use custom user defined tools from Tools.txt add Tools Component to Grasshopper canvas and assign the correct tool-path. After this all components will know about the set of tools, because we set a static dictionary.

<img width="1440" alt="Screenshot 2022-04-24 at 18 45 45" src="https://user-images.githubusercontent.com/18013985/164987154-4a0c4a6b-b400-4037-ac68-a8d8b56c3e66.png">

# Example Files
![CNC_Plugin](https://user-images.githubusercontent.com/18013985/164996185-eb7d612d-bc5b-4e32-94a5-09fef6dd9750.png)
https://user-images.githubusercontent.com/18013985/164996235-c1af4799-1e8e-488e-8755-79c49566c315.mp4



### Physical Work
- [x] Fabricate table [Video](https://vimeo.com/645880001 "Fabricate table - Click to Watch!")
- [ ] camera holder, buy cameras

### Code

- [x] C A axis implementation
- [x] B-Axis integration ( a)find most top position, b) rotate b axis ) [Video](https://vimeo.com/645879445 "B-Axis integration - Click to Watch!")
- [ ] calibration procedure
- [ ] camera
- [x] milling 
- [x] slice
- [x] notches [Video](https://vimeo.com/645882287 "notches - Click to Watch!")
- [x] probe 
- [ ] drilling using I J (use G48 for cutting in a plane)
- [ ] rotation limits in 5 axis (did not popped yet)
- [x] collision detection model for the Cardan 5-Axis CNC machine [Video](https://vimeo.com/647108247 "Cardan Axis Approximation - Click to Watch!")
- [x] simulation update in Visual Studio Cardan
- [ ] air supply
- [x] text use OpenNest 
- [ ] too deep angle cut
- [ ] P4010:-310 plastic cover down

### What do you need to know when using Cardar Axis

Do not interpolate between two different angles, because you will get a curve cut:
![image](https://user-images.githubusercontent.com/18013985/166113018-1f805a70-3f01-4619-b8f8-f2089a739883.png)

### Acceess to CNC server:
\\128.178.35.2\ncdata
