export function debounce(
  func: (...args: any) => any,
  wait: number,
  immediate: boolean
) {
  let timeout: any;
  return function () {
    // @ts-ignore
    const context = this,
      args = arguments;
    clearTimeout(timeout);
    timeout = setTimeout(function () {
      timeout = null;
      // @ts-ignore
      if (!immediate) func.apply(context, args);
    }, wait);
    // @ts-ignore
    if (immediate && !timeout) func.apply(context, args);
  };
}
