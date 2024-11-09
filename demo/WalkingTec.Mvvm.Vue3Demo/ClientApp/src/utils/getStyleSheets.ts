import { nextTick } from 'vue';
import * as svg from '@element-plus/icons-vue';

// 获取阿里字体图标
const getAlicdnIconfont = () => {
	return new Promise((resolve, reject) => {
		nextTick(() => {
            const styles: any = document.styleSheets;
			let sheetsIconList = [];

            for (let i = 0; i < styles.length; i++) {
                for (let j = 0; j < styles[i].cssRules.length; j++) {
                    if (styles[i].cssRules[j].selectorText && styles[i].cssRules[j].selectorText.indexOf('._wtmicon-') > -1) {
						sheetsIconList.push(
                            `${styles[i].cssRules[j].selectorText.substring(1, styles[i].cssRules[j].selectorText.length).replace(/\:\:before/gi, '')}`
						);
					}
				}
			}
			if (sheetsIconList.length > 0) resolve(sheetsIconList);
			else reject('未获取到值，请刷新重试');
		});
	});
};

// 初始化获取 css 样式，获取 element plus 自带 svg 图标，增加了 ele- 前缀，使用时：ele-Aim
const getElementPlusIconfont = () => {
	return new Promise((resolve, reject) => {
		nextTick(() => {
			const icons = svg as any;
			const sheetsIconList = [];
			for (const i in icons) {
				sheetsIconList.push(`ele-${icons[i].name}`);
			}
			if (sheetsIconList.length > 0) resolve(sheetsIconList);
			else reject('未获取到值，请刷新重试');
		});
	});
};

// 初始化获取 css 样式，这里使用 fontawesome 的图标
const getAwesomeIconfont = () => {
    return new Promise((resolve, reject) => {
        nextTick(() => {
            const styles: any = document.styleSheets;
            let sheetsIconList = [];
            for (let i = 0; i < styles.length; i++) {
                for (let j = 0; j < styles[i].cssRules.length; j++) {
                    let selectorText = styles[i].cssRules[j].selectorText
                    if (
                        selectorText &&
                        selectorText.includes('.fa-')
                    ) {
                        let newSelectorTextList: [string] = selectorText.split(", ")
                        for (let k = 0; k < newSelectorTextList.length; k++) {
                            const element = newSelectorTextList[k];
                            if (/::before/.test(element)) {
                                sheetsIconList.push(
                                    `${element.substring(1, element.length).replace(/\:\:before/gi, '')}`
                                );
                            }
                        }
                    }
                }
            }
            if (sheetsIconList.length > 0) resolve(sheetsIconList.reverse());
            else reject('未获取到值，请刷新重试');
        });
    });
};

/**
 * 获取字体图标 `document.styleSheets`
 * @method ali 获取阿里字体图标 `<i class="iconfont 图标类名"></i>`
 * @method ele 获取 element plus 自带图标 `<i class="图标类名"></i>`
 * @method ali 获取 fontawesome 的图标 `<i class="fa 图标类名"></i>`
 */
const initIconfont = {
	// iconfont
	ali: () => {
		return getAlicdnIconfont();
	},
	// element plus
	ele: () => {
		return getElementPlusIconfont();
	},
	// fontawesome
	awe: () => {
		return getAwesomeIconfont();
	},
};

// 导出方法
export default initIconfont;
