<div align='center'>
    <h1>
        HCE LAA
    </h1>
    <p>
        Apply LAA patches to HCE/SPV3/MCC executables.
    </p>
</div>

## Introduction

This project enables the LAA (Large Address Aware) flag on HCE, SPV3 and MCC-related executables. This is particularly useful for development tools (e.g. `tool.exe`) to improve stability when dealing with larger assets.

## Installation & Usage

1. Place the program and the JSON files in your MCC/HCE/SPV3 directory.
2. Run the executable. You may have to run the program as an administrator if dealing with restricted directories.
3. All of the known executables in the directory will be patched. For safety, they will first be backed up to a `.gz` archive.

## Supported Executables

This section describes the executables which this project will patch with the LAA flag.

### MCC

| Executable      | CRC32      |
| --------------- | ---------- |
| `tool.exe`      | `7ce911cb` |
| `tool_fast.exe` | `60e98f82` |

## Patching System

Patching specifications for each executable are stored in the corresponding JSON file for HCE, SPV3 and MCC:

| Section | Patch Specification                  |
| ------- | ------------------------------------ |
| MCC     | [`mcc-laa.json`](./laa/mcc-laa.json) |