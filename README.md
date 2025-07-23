# BletchleyMaker
![Program Demo](./image.gif)

A personal project aimed at RAFAC (RAF Air Cadet) code-breaking exercises. The purpose of this tool is to encode sentences and create decode grids for the Air Cadet Bletchley Park cipher. This automates the process of making codes for Air Cadet activities.

![Build Status](https://img.shields.io/badge/build-passing-brightgreen) ![License](https://img.shields.io/badge/license-MIT-blue)

## Features Available
- Encoding plaintext sentences into Bletchley Park ciphertext
- Generating a randomised decryption table (6x6 A-Z 0-9)
- Decoding input ciphertext
- Saving, opening and printing grids (Printing ONLY works in A4)

## Tool Use
To install the application, download the MSI file [here](https://github.com/shr0m/BletchleyMaker/releases).
The program should automatically update the existing files to a newer version. If this does not work:
- Win+S, then search for 'Add or Remove Programs'
- Scroll or search 'BletchleyMaker' and uninstall the old version
- Rerun the MSI installer

### Basic guide:
- Generate: Generates a new random 6x6 grid including letters A-Z and 0-9
- Execute: Takes string from 'Text to Convert' and applies the reverse of the decode rule to encode
- Decode: If checked, will decode string from 'Text to Convert' with the decode rule as entered
- Save Code: Will add the code in Output to print with the grid
- Hide index: Will remove the index of spaces from the saved code. For example 'L2' would become 'L'
- Randomise rule: Will ignore any value in the rulebox and create a random rule value to use
- Menu: Provides additional functionality, such as printing, reporting errors, and creating own grids

## Security
Please be aware that this application is not CA signed, and may be flagged by antivirus. If you have any queries about the security of this application, please contact the developer within the support section of this file. It is recommended that all files which you download are scanned by VirusTotal. The upload page to check for malware can be found [here](https://www.virustotal.com/gui/home/upload)

## License
See [LICENSE](./LICENSE) for information regarding license(s)

## Support
Any issues can be reported in the issues tab of this repository. However, the developer may also be contacted through: jan.korzybski@proton.me
Requests can be made for other features and updates.

## About RAFAC
The [Royal Air Force Air Cadets](https://www.raf.mod.uk/aircadets/) is the combined volunteer-military youth organisation sponsored by the Royal Air Force, which is formed by both the Air Training Corps and RAF Sections of the Combined Cadet Force. The organisation is headed by a former serving RAF officer, Commandant Air Cadets.
