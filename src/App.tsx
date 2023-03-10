import { Fragment, useEffect, useState, useCallback } from 'react';
import { Unity, useUnityContext } from 'react-unity-webgl';

function App() {
  const {
    unityProvider,
    isLoaded,
    loadingProgression,
    sendMessage,
    addEventListener,
    removeEventListener,
  } = useUnityContext({
    loaderUrl:
      'unity/guildfi-unity-poc-alpha/Build/guildfi-unity-poc-alpha.loader.js',
    dataUrl: 'unity/guildfi-unity-poc-alpha/Build/guildfi-unity-poc-alpha.data',
    frameworkUrl:
      'unity/guildfi-unity-poc-alpha/Build/guildfi-unity-poc-alpha.framework.js',
    codeUrl: 'unity/guildfi-unity-poc-alpha/Build/guildfi-unity-poc-alpha.wasm',
    streamingAssetsUrl: 'unity/guildfi-unity-poc-alpha/StreamingAssets',
  });

  const [devicePixelRatio, setDevicePixelRatio] = useState(
    window.devicePixelRatio
  );

  const handleChangePixelRatio = useCallback(
    function () {
      // A function which will update the device pixel ratio of the Unity
      // Application to match the device pixel ratio of the browser.
      const updateDevicePixelRatio = function () {
        setDevicePixelRatio(window.devicePixelRatio);
      };
      // A media matcher which watches for changes in the device pixel ratio.
      const mediaMatcher = window.matchMedia(
        `screen and (resolution: ${devicePixelRatio}dppx)`
      );
      // Adding an event listener to the media matcher which will update the
      // device pixel ratio of the Unity Application when the device pixel
      // ratio changes.
      mediaMatcher.addEventListener('change', updateDevicePixelRatio);
      return function () {
        // Removing the event listener when the component unmounts.
        mediaMatcher.removeEventListener('change', updateDevicePixelRatio);
      };
    },
    [devicePixelRatio]
  );

  const [modelLoadPercentage, setModelLoadPercentage] = useState(100);

  useEffect(() => {
    addEventListener('SetModelLoadPercentage', setModelLoadPercentage);
    return () => {
      removeEventListener('SetModelLoadPercentage', setModelLoadPercentage);
    };
  }, [addEventListener, removeEventListener, setModelLoadPercentage]);

  const loadingPercentage = Math.round(loadingProgression * 100);

  return (
    <div className="h-screen select-none">
      <div className="max-w-lg w-full m-auto h-full max-h-[48rem] rounded-lg border-2 bg-slate-200 flex flex-col">
        <div className="flex bg-gray-200 text-xl m-auto w-fit my-2 ">
          Unity PoC
        </div>
        <div className="flex bg-gray-200 text-xl m-auto w-fit my-2 ">
          {modelLoadPercentage != 100 && (
            <Fragment>Loading: ({modelLoadPercentage}%)</Fragment>
          )}
          {modelLoadPercentage == 100 && <Fragment>Ready!</Fragment>}
        </div>
        <div className="flex flex-row">
          {[...Array(5)].map((_, i) => (
            <button
              key={i}
              className="w-fit my-2 mx-auto border-black border-2 p-2 rounded-md hover:bg-slate-300
            active:bg-slate-800"
              onClick={() => {
                sendMessage('Loader', 'InstantiateAsync', i);
              }}
            >
              Load Model {i}
            </button>
          ))}
        </div>
        <div className="w-full mx-auto p-2 h-full bg-gray-300">
          {isLoaded === false && (
            <div className="w-fit h-full text-xl justify-items-center items-center flex mx-auto">
              Loading... ({loadingPercentage}%)
            </div>
          )}
          <Unity
            unityProvider={unityProvider}
            className="w-full h-full"
            devicePixelRatio={devicePixelRatio}
          />
        </div>
      </div>
    </div>
  );
}

export default App;
/**/
