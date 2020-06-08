import { Definitions } from './utilities/project';

export const defaults = {
  // 'node/index.d.ts': 'https://unpkg.com/@types/node/index.d.ts',
};

export const threeDefinitions: Definitions = {
  'three/index.d.ts': 'https://unpkg.com/@types/three/index.d.ts',
  'three/detector.d.ts': 'https://unpkg.com/@types/three/detector.d.ts',
  'three/three-FirstPersonControls.d.ts':
    'https://unpkg.com/@types/three/three-FirstPersonControls.d.ts',
  'three/three-canvasrenderer.d.ts': 'https://unpkg.com/@types/three/three-canvasrenderer.d.ts',
  'three/three-colladaLoader.d.ts': 'https://unpkg.com/@types/three/three-colladaLoader.d.ts',
  'three/three-copyshader.d.ts': 'https://unpkg.com/@types/three/three-copyshader.d.ts',
  'three/three-css3drenderer.d.ts': 'https://unpkg.com/@types/three/three-css3drenderer.d.ts',
  'three/three-ctmloader.d.ts': 'https://unpkg.com/@types/three/three-ctmloader.d.ts',
  'three/three-editorcontrols.d.ts': 'https://unpkg.com/@types/three/three-editorcontrols.d.ts',
  'three/three-effectcomposer.d.ts': 'https://unpkg.com/@types/three/three-effectcomposer.d.ts',
  'three/three-maskpass.d.ts': 'https://unpkg.com/@types/three/three-maskpass.d.ts',
  'three/three-octree.d.ts': 'https://unpkg.com/@types/three/three-octree.d.ts',
  'three/three-orbitcontrols.d.ts': 'https://unpkg.com/@types/three/three-orbitcontrols.d.ts',
  'three/three-orthographictrackballcontrols.d.ts':
    'https://unpkg.com/@types/three/three-orthographictrackballcontrols.d.ts',
  'three/three-projector.d.ts': 'https://unpkg.com/@types/three/three-projector.d.ts',
  'three/three-renderpass.d.ts': 'https://unpkg.com/@types/three/three-renderpass.d.ts',
  'three/three-shaderpass.d.ts': 'https://unpkg.com/@types/three/three-shaderpass.d.ts',
  'three/three-trackballcontrols.d.ts': 'https://unpkg.com/@types/three/three-trackballcontrols.d.ts',
  'three/three-transformcontrols.d.ts': 'https://unpkg.com/@types/three/three-transformcontrols.d.ts',
  'three/three-vrcontrols.d.ts': 'https://unpkg.com/@types/three/three-vrcontrols.d.ts',
  'three/three-vreffect.d.ts': 'https://unpkg.com/@types/three/three-vreffect.d.ts',
};

export const preactDefinitions = { 'preact/preact.d.ts': 'https://unpkg.com/preact/src/preact.d.ts' };

export const reactDefinitions = {
  'react/index.d.ts': 'https://unpkg.com/@types/react/index.d.ts',
};

export const reactDomDefinitions = {
  'react-dom/index.d.ts': 'https://unpkg.com/@types/react-dom/index.d.ts',
  'react-dom/server.d.ts': 'https://unpkg.com/@types/react-dom/server.d.ts',
};

export const pixijsDefinitions = { 'pixi.js/index.d.ts': 'https://unpkg.com/@types/pixi.js/index.d.ts' };

export const reglDefinitions = {
  'regl/index.d.ts': 'https://raw.githubusercontent.com/jmfirth/DefinitelyTyped/regl-support/types/regl/index.d.ts'
};

export const glMatrixDefinitions = { 'gl-matrix/index.d.ts': 'https://unpkg.com/@types/gl-matrix/index.d.ts' };

export const lodashDefinitions = { };

export const definitionList: { [moduleName: string]: Definitions } = {
  'three': threeDefinitions,
  // 'preact': preactDefinitions,
  // 'react': reactDefinitions,
  // 'react-dom': reactDomDefinitions,
  // 'pixi.js': pixijsDefinitions,
  // 'gl-matrix': glMatrixDefinitions,
  'regl': reglDefinitions,
  // 'lodash': lodashDefinitions,
};

export async function findDefinition(moduleName: string) {
  const urls = [
    `https://unpkg.com/@types/${moduleName}/index.d.ts`,
    `https://unpkg.com/${moduleName}/dist/index.d.ts`,
    `https://unpkg.com/${moduleName}/dist/${moduleName}.d.ts`,
    `https://unpkg.com/${moduleName}/src/index.d.ts`,
    `https://unpkg.com/${moduleName}/src/${moduleName}.d.ts`,
    `https://unpkg.com/${moduleName}/index.d.ts`,
    `https://unpkg.com/${moduleName}/${moduleName}.d.ts`,
  ];
  for (let i = 0; i < urls.length; i++) {
    try {
      const res = await fetch(urls[i]);
      const text = await res.text();
      if (!text.startsWith('Not found: package')) { return urls[i]; }
    } catch (e) {/* ... */}
  }
  return;
}