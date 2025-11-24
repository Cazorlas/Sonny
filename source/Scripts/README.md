# Scripts

Utility scripts for the Sonny project.

## resize_icons.py

Resize icon images from 1024x1024 to 16x16 and 32x32 pixels.

### Requirements

```bash
pip install Pillow
```

### Usage

```bash
# Resize icon and save to same directory
python resize_icons.py icon.png

# Resize icon and save to specific directory
python resize_icons.py icon.png output/
```

### Output

- `icon16.png` - 16x16 pixels
- `icon32.png` - 32x32 pixels

