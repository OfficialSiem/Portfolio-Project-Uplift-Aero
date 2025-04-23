# Portfolio-Project-Uplift-Aero
 Portfolio Project for Uplift Aerospace Interview Process

## What is this for?
This is a public project requested for the next step in Uplift Aerospace's hiring process. As part of this stage, I was asked to submit a code sample, and a single PR, that constitutes about 2-to-3 hours of work in order to give Uplift Aero some insight into how I think and work.

## Project Description - From Scratch "AI Photobooth"

Earlier, I found a reddit user who [made a tool that does most of the heavy lifting for unity and ai generated backgrounds](https://www.reddit.com/r/Unity3D/comments/1fi8mvu/finally_made_a_tool_that_does_most_of_the_heavy/) 

Inspired by their work, I'd like to take the time to build a similar tool and perhaps deploy it for VR Photobooth applications!

Users would be able to snap photos of themselves in a Generative AI-Landscape, potentially drop assets or invite guests to build memories together!

## How AI is implemented

We're taking a rendered frame from Unity’s Main Camera, sending it to a Hugging Face model (like a text-guided or reference-based image generator), and overlaying the result in real-time (or near real-time) in the game's viewport.

Think of this as generating an AI-stylized, interpretation, of the current scene using the environment as context.

### Image Reference to Explore Thinking Process/Explaining A Plan of Action

![Image](https://github.com/user-attachments/assets/81fdeab5-2aae-49a2-bb04-caafbb20a5a5)

## Requirements

- Unity Hub
- Unity (Editor Version 2022.3.61f1 or higher)
- GitHub Desktop or favorite git tool.


## Installation

Use either your favorite git tool or GitHub Desktop to download this Demo Unity Project into your computer or local server. 

Once your download has finished, you can open this project through Unity Hub. 

### Using Unity Hub Instructions (Works as of 4.22.2025)

1. When Unity Hub loads a list of projects to open, navigate to the "Add" button located to the upper-right of the program.
2. Once the "Add" button is clicked, a drop-down menu should open up allowing you to select. "Add Project From Disk" 
3. Navigate to the root folder of this project. It should look like ```something\something\Portfolio Project Uplift Aero```
4. Unity Hub will add it to your list of Projects. Tap on the new project to open inside the editor; have fun and explore!

## Contributing

Pull requests are welcome. For major changes, please open an issue first
to discuss what you would like to change.

Please make sure to update tests as appropriate, especially if you implement or edit any tests when you push.

## License

[MIT](https://choosealicense.com/licenses/mit/)
