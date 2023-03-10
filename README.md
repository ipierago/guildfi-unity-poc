# guildfi-unity-poc

Welcome!  This is the GuildFi Unity Proof of Concept.  It demonstrates the following:
- Unity WebGL app running in a React app
- Communication between Unity and React
- On-demand loaded assets from CDN
- Simple toon shader

There are two main parts to this project: the Unity project and the React project.  The Unity project is in the unity folder.  The React project is in the workspace root.

The latest build of the Unity project is already in the public folder.  Unless you are working on the Unity project, you do not need to build it.  You can just run the React app.

## Setup

From the workspace root:
> yarn

If you plan on working on the Unity project, you will need to install Unity.  The version used for this project is 2020.3.0f1.

## To Debug and/or Build Unity

There are two things that need to be built: addressable bundles and the unity build itself.  The addressable bundles must also be deployed to the CDN for a full build.

If you are just debugging within the editor, you only need to build the addressable bundles.  You can skip  uploading addressable bundles to the CDN.  The addressables bundles will be loaded from the local file system.  You also don't need to do the Unity build to debug in the editor (obviously).

1. Open unity/guildfi-unity-poc-alpha in Unity
2. Windows->Asset Management->Addressables->Groups->Build

At this point, you can debug in the editor.  Do the remaining steps to complete the build for use in the React app.

3. Build the player with WebGL target to public/unity/guildfi-unity-poc-alpha
4. Windows->Asset Management->Adressables->Groups->Build to CCD

## To Debug the Full App

From the workspace root:
> yarn dev

## To Build and Deploy the Full App

From the workspace root:
> yarn build

You can now serve the dist folder if you'd like.

> firebase deploy

## TODO
- Shaders as addressables in default bundle
- try performance on low end phone
- Compare against threejs / babylonjs
- Build multiple version of the Unity app for different texture compressions - DXT1 (desktop), ETC (mobile), ASTC (mobile)
- Progressively loaded models and/or texures? Is it needed?

## ISSUES
- models in addressable bundles are much larger than expected
  