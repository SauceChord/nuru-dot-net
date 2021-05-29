# nuru File specifications

All data is represented as little endian unless specified otherwise in the description fields.

## Image file (*.nui)

### Header (32 bytes)

| Offset | Field                | Length (bytes) | Type     | Description          |
|--------|----------------------|----------------|----------|----------------------|
| `0x00` | Signature            | `7`            | ASCII    | `"NURUIMG"`          |
| `0x07` | Version              | `1`            | `byte`   | (Supports `1`)       |
| `0x08` | Glyph mode           | `1`            | `byte`   | ![Diagram](http://www.plantuml.com/plantuml/svg/SoWkIImgAStDuKhDAyrLSC-fBCZmpKz9LQZcKb3moyzBLR1LC00o7OETFJs1R4CWEpGlCnce1CWp0l824dDIIqfItJ9pKa7oHfPSjLmEgNafG3C1) <br>**Color mode** and **Glyph mode** can't both be `None` (`0`) |
| `0x09` | Color mode           | `1`            | `byte`   | ![Diagram](http://www.plantuml.com/plantuml/svg/SoWkIImgAStDuKhDAyrLSCxFoI_opKz9LQZcKb3moyzBLR1LC00o7Vs2FKs0R4CesmB8DWAo0n9pKajAKjqoSr91iiO6NBLS3gbvAK0J0G00) <br>**Color mode** and **Glyph mode** can't both be `None` (`0`) |
| `0x0A` | Metadata mode        | `1`            | `byte`   | ![Diagram](http://www.plantuml.com/plantuml/svg/SoWkIImgAStDuKhDAyrLy4qjIVJDJqbLgEPIKF3BpqjLi5Km0393FVDAB81iGoZRq0p8CUAgvN98pKi1wWO0) |
| `0x0B` | Width                | `2`            | `ushort` | Big endian encoded.  |
| `0x0D` | Height               | `2`            | `ushort` | Big endian encoded.  |
| `0x0F` | Key character        | `1`            | ASCII    | Characters in image matching this are to be converted to space.<br>Defaults to `' '`. |
| `0x10` | Key foreground       | `1`            | `byte`   | Foreground color in image matching this are to be rendered using the clients default foreground color.<br>Defaults to `15`. |
| `0x11` | Key background       | `1`            | `byte`   | Background color in image matching this are to be rendered using the clients default background color.<br>Defaults to `0`. |
| `0x12` | Glyph palette        | `7`            | ASCII    | A zero terminated filename without `.nup` extension to be loaded and used if **Glyph mode** is **PaletteFile** (`129`). |
| `0x19` | Color palette        | `7`            | ASCII    | A zero terminated filename without `.nup` extension to be loaded and used if **Color mode** is **PaletteFile** (`130`). |

### Contents (varying length)

- **Width** times **Height** entries.
- Entries are laid out left to right, top to bottom.
- Entries contain the following triplets in order: *Glyph*, *Color*, *Metadata*.
- Components of triplets are optional and of varying length according to modes set in the header.

#### Glyph
| Glyph mode  | Value | Length | Description                          |
|-------------|-------|--------|--------------------------------------|
| None        | 0     | `0`    | No data.                             |
| ASCII       | 1     | `1`    |                                      |
| Unicode     | 2     | `2`    | Big endian encoded.                  |
| PaletteFile | 129   | `1`    | Index into nup files' glyph palette. |

#### Color
| Color mode  | Value | Length | Description                                          |
|-------------|-------|--------|------------------------------------------------------|
| None        | 0     | `0`    | No data.                                             |
| ANSI4       | 1     | `1`    | High nibble is foreground, low nibble is background. |
| ANSI8       | 2     | `2`    | 1st byte foreground, 2nd byte background.            |
| PaletteFile | 130   | `1`    | Index into nup files' color palette.                 |

#### Metadata
| Metadata mode  | Value | Length | Description                |
|----------------|-------|--------|----------------------------|
| None           | 0     | `0`    | No data.                   |
| UInt8          | 1     | `1`    |                            |
| UInt16         | 2     | `2`    | Big endian encoded ushort. |

## Palette file (*.nup)

Todo

### Header (16 bytes)

Todo

### Contents (256, 512 or 768 bytes)

Todo
