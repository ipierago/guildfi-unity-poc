import { Unity, useUnityContext } from 'react-unity-webgl';

function App() {
  const { unityProvider } = useUnityContext({
    loaderUrl:
      'unity/guildfi-unity-poc-alpha/Build/guildfi-unity-poc-alpha.loader.js',
    dataUrl: 'unity/guildfi-unity-poc-alpha/Build/guildfi-unity-poc-alpha.data',
    frameworkUrl:
      'unity/guildfi-unity-poc-alpha/Build/guildfi-unity-poc-alpha.framework.js',
    codeUrl: 'unity/guildfi-unity-poc-alpha/Build/guildfi-unity-poc-alpha.wasm',
  });

  return (
    <Unity unityProvider={unityProvider} style={{ height: 600, width: 800 }} />
  );
}

export default App;
