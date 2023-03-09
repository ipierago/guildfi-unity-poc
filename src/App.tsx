import { Unity, useUnityContext } from 'react-unity-webgl';

function App() {
  const { unityProvider, isLoaded, loadingProgression, sendMessage } =
    useUnityContext({
      loaderUrl:
        'unity/guildfi-unity-poc-alpha/Build/guildfi-unity-poc-alpha.loader.js',
      dataUrl:
        'unity/guildfi-unity-poc-alpha/Build/guildfi-unity-poc-alpha.data',
      frameworkUrl:
        'unity/guildfi-unity-poc-alpha/Build/guildfi-unity-poc-alpha.framework.js',
      codeUrl:
        'unity/guildfi-unity-poc-alpha/Build/guildfi-unity-poc-alpha.wasm',
      streamingAssetsUrl: 'unity/guildfi-unity-poc-alpha/StreamingAssets',
    });

  const loadingPercentage = Math.round(loadingProgression * 100);

  return (
    <div className="h-screen select-none">
      <div className="max-w-lg w-full m-auto h-full max-h-[48rem] rounded-lg border-2 bg-slate-200 flex flex-col">
        <div className="flex bg-gray-200 text-xl m-auto w-fit my-2 ">
          Unity PoC
        </div>
        <div className="flex flex-row">
          <button
            className="w-fit my-2 mx-auto border-black border-2 p-2 rounded-md hover:bg-slate-300
        active:bg-slate-800"
            onClick={() => {
              sendMessage('Loader', 'InstantiateAsync', 0);
            }}
          >
            Load Model 0
          </button>
          <button
            className="w-fit my-2 mx-auto border-black border-2 p-2 rounded-md hover:bg-slate-300
        active:bg-slate-800"
            onClick={() => {
              sendMessage('Loader', 'InstantiateAsync', 1);
            }}
          >
            Load Model 1
          </button>
          <button
            className="w-fit my-2 mx-auto border-black border-2 p-2 rounded-md hover:bg-slate-300
        active:bg-slate-800"
            onClick={() => {
              sendMessage('Loader', 'InstantiateAsync', 2);
            }}
          >
            Load Model 2
          </button>
        </div>
        <div className="w-full mx-auto p-2 h-full bg-gray-300">
          {isLoaded === false && (
            <div className="w-fit h-full text-xl justify-items-center items-center flex mx-auto">
              Loading... ({loadingPercentage}%)
            </div>
          )}
          <Unity unityProvider={unityProvider} className="w-full h-full" />
        </div>
      </div>
    </div>
  );
}

export default App;
/**/
