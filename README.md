> [!NOTE]
> **Please carefully read ALL the _setup instructions_, as we are using a Custom Build of Godot.**
>
> Our Godot Build was last updated on the <ins>18/09/2024</ins>.

# EnVRonment Editor

This is the repository for the EnVRonment Editor, a Godot-based editor for the EnVRonment platform.

Documentation https://editor.docs.envronment.com

## Setup

### How to setup Godot for development

Download our [Custom Build of Godot 4.3.1 from EnVRonment S3](https://s3.envronment.com/download-public/godot-4.3.1.7z).

You can extract it in a directory located anywhere, this is your *Godot install folder*.

After that, you'll need to setup the csharp dependencies with the following commands,
the `godot-nuget` is the absolute path to the directory `GodotNuget` in the archive:

```bat
dotnet nuget add source <godot-nuget> --name GodotNuget
```

From this folder, run Godot from `godot.windows.editor.x86_64.mono.exe`.

### How to setup Godot to export and build (Windows)

From your Godot install folder, run the following commands to create your prepare your build preset. **You have to do it each time you use a new build of Godot.**

CMD.exe:
```bat
mkdir %APPDATA%\Godot\export_templates\4.3.1.rc.mono
copy godot.windows.template_debug.x86_64.mono.exe %APPDATA%\Godot\export_templates\4.3.1.rc.mono\windows_debug_x86_64.exe
copy godot.windows.template_release.x86_64.mono.exe %APPDATA%\Godot\export_templates\4.3.1.rc.mono\windows_release_x86_64.exe
pause
```

In Godot, create a build preset: Project > Export as... > Add... > Windows Desktop. Your build should then work.

**For production builds only:** You also have to download [rcedit-x64.exe](https://github.com/electron/rcedit/releases). After that put the path of the executable in Godot: Editor > Editor Settings... > Export > Windows > Rcedit.

## TODO

- [ ] UI/UX
  - [ ] Add a splash screen like Material Maker
  - [ ] Add a loading screen
  - [ ] Add a settings menu
  - [ ] Add a toolbar
  - [ ] Add a console?
  - [ ] Add toasts
  - [ ] Layout
    - [ ] Add a flexible layout to be able to resize and move the panels around (with same)
    - [ ] Add an inspector panel
    - [ ] Add a scene tree panel
    - [ ] Add a file system panel
    - [ ] Add a console panel
    - [ ] Update the game view to have widgets on it
- [ ] Inspector
  - [ ] Add a way to see the properties of the selected node
  - [ ] Add a way to edit the properties of the selected node
- [ ] Scene Tree
  - [ ] Add a way to see the hierarchy of the scene
  - [ ] Add a way to select a node
  - [ ] Add a way to move a node
  - [ ] Add a way to delete a node
  - [ ] Add a way to add a node
- [ ] File System
  - [ ] Add a way to see the files in the project
  - [ ] Add a way to open a file
  - [ ] Add a way to save a file
  - [ ] Add a way to create a file
  - [ ] Add a way to delete a file
- [ ] Visual Scripting
  - [ ] Implement the TCP protocol or add a webview to the editor
- [ ] Gizmos
  - [ ] Fix the issue with gltf models, gizmos are misplaced
  - [ ] Click outside the gizmo to deselect it
  - [ ] Fix offset
- [ ] History
    - [ ] Add a way to undo
    - [ ] Add a way to redo
    - [ ] Add a way to see the history
    - [ ] Add a way to clear the history
    - [ ] Add a way to limit the history (configurable)
- [ ] Save a project
- [ ] Load a project
- [ ] Checkpoints
- [ ] Autosave
- [ ] Keyboard shortcuts
  - [ ] Add a way to see the keyboard shortcuts
  - [ ] Add a way to change the keyboard shortcuts
- [ ] Console
  - [ ] Add a way to see the logs
  - [ ] Add a way to clear the logs
  - [ ] Add a way to filter the logs