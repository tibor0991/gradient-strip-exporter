# gradient-strip-exporter
A simple Unity tool that creates a texture strip out of a Gradient value.

## Interface

### Settings
- **Name**: the name of the texture;
- **Extension**: which extension should be used for the texture (supports PNG, JPG, TGA);
- **Resolution**: the width in pixel of the strip (all strips are 1 px high);
- **Gradient**: the gradient we're going to export.

### Export
- **File path**: the absolute path of the folder in which the strip will be saved;
- **Folder**: opens the folder picker;
- **Save Texture**: creates a *Resolution* by 1 texture with the chosen gradient.

## How to use
After the installation, the tool can be found in Window > Gradient Strip Exporter.
Simply fill in the settings with the required values, choose a save path and click **Save Texture**.

## Future updates
- Provide the possibility to automatically set the import options of the newly-created strips.
- Properly expose the Gradient-To-Texture2D methods so that they can be used also outside of the tool.
