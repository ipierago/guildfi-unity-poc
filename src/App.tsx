/*
function App() {
  return (
    <div className="bg-gray-500 flex flex-col h-screen">
      <div className="flex h-32 bg-gray-200"></div>
      <div className="flex-1 w-2/3 mx-auto p-4 text-lg bg-white h-full shadow-lg bg-gray-300">
        foo
      </div>
    </div>
  );
}

export default App;
*/

/**/
import { Unity, useUnityContext } from 'react-unity-webgl';

function App() {
  const { unityProvider } = useUnityContext({
    loaderUrl:
      'unity/guildfi-unity-poc-alpha/Build/guildfi-unity-poc-alpha.loader.js',
    dataUrl: 'unity/guildfi-unity-poc-alpha/Build/guildfi-unity-poc-alpha.data',
    frameworkUrl:
      'unity/guildfi-unity-poc-alpha/Build/guildfi-unity-poc-alpha.framework.js',
    codeUrl: 'unity/guildfi-unity-poc-alpha/Build/guildfi-unity-poc-alpha.wasm',
    streamingAssetsUrl: 'unity/guildfi-unity-poc-alpha/StreamingAssets',
  });

  return (
    <div className="h-screen select-none">
      <div className="max-w-lg w-full m-auto h-full max-h-[48rem] rounded-lg border-2 bg-slate-200 flex flex-col">
        <div className="flex bg-gray-200 text-2xl m-auto w-fit my-2 ">
          Unity PoC
        </div>
        <div className="flex flex-row">
          <button
            className="w-fit my-2 mx-auto border-black border-2 p-2 rounded-md hover:bg-slate-300
        active:bg-slate-800"
            onClick={() => {
              console.log('foo');
            }}
          >
            Load Model
          </button>

          <button
            className="w-fit my-2 mx-auto border-black border-2 p-2 rounded-md hover:bg-slate-300
        active:bg-slate-800"
            onClick={() => {
              console.log('ClearModel');
            }}
          >
            Clear Model
          </button>

          <button
            className="w-fit my-2 mx-auto border-black border-2 p-2 rounded-md hover:bg-slate-300
        active:bg-slate-800"
            onClick={() => {
              console.log('Fullscreen');
            }}
          >
            Fullscreen
          </button>
        </div>
        <Unity
          unityProvider={unityProvider}
          className="w-full mx-auto p-2 h-full bg-gray-300"
          //className="max-w-[12rem]"
        />
      </div>
    </div>
  );
}

export default App;
/**/
