import { immerObservable} from './store.js';
import {derived} from 'svelte/store';
import {state} from './state.js';

//should come from the server without testSelected or id, which should be mapped onto the results
export const availableTests = immerObservable([
	{	
    testSelected:false,
		testName:"Digital Input 1 Hi",
		upperLimit:"PASS",
		testValue:"",
		lowerLimit:"PASS",
		result:"",
    testStatus:"",
    id:0,
    testValueType:"PassFail"
  },
	{	
    testSelected:false,
		testName:"Digital Input 2 Hi",
		upperLimit:"PASS",
		testValue:"",
		lowerLimit:"PASS",
		result:"",
    testStatus:"",
    id:1,
    testValueType:"PassFail"
  },
	{	
    testSelected:true,
		testName:"Analog Input 1, 0-5V",
		upperLimit:"4.5",
		testValue:"",
		lowerLimit:"4.7",
		result:"",
    testStatus:"",
    id:2,
    testValueType:"Double"
  }
    ]);

export const selectedTests =  derived([state, availableTests], ([state, availableTests]) => availableTests.find(v => v.testSelected === true));

export const selectTest =  i => {
  availableTests.update(draft => {
        draft.find(v=> v.id === i).testSelected = true;
    });
};
export const unSelectTest =  i => {
  availableTests.update(draft => {
        draft.find(v=> v.id === i).testSelected = false;
    });
};
// export const changeUser = i => {
//   availableTests.update(draft => {
//       draft.currentUser = i;
//   });
// };
export const sortAvailableTests = ev => {
  availableTests.update(draft => {
      console.log("sort order draft", ev)
      draft = ev.detail;
      console.log("sort order draft", draft)

  });
};
