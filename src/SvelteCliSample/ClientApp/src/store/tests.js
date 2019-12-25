import {derived, writable} from 'svelte/store';

    // onMount(() => {
    //     fetch('/api/tests')
    //         .then(response => response.json())
    //         .then(data => {
    //             availableTests = data;
    //         });
    // });

//should come from the server without testSelected or id, which should be mapped onto the results
export const availableTests = writable([
	{	
    testSelected:true,
		testName:"Digital Input 1 Hi",
		upperLimit:"PASS",
		testValue:"",
		lowerLimit:"PASS",
		result:"",
    testStatus:"",
    id:0,
    testValueType:"PassFail",
    sortId:0
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
    testValueType:"PassFail",
    sortId:1
  },
	{	
    testSelected:false,
		testName:"Analog Input 1, 0-5V",
		upperLimit:"4.5",
		testValue:"",
		lowerLimit:"4.7",
		result:"",
    testStatus:"",
    id:2,
    testValueType:"Double",
    sortId:2
  }
    ]);

// export const selectedTests =  derived(
//   availableTests,
//   $availableTests => {
//     return filterBySelected($availableTests)
//   })

export const availableTest = derived(
  availableTests,
  $availableTests => {
    return (availableTests.f)
  }
)

function filterBySelected(availableTests){
  return availableTests.filter(test => test.testSelected)
} 

function setById(ID){
  return (availableTests.filter(test => test.id === ID).testSelected = true)
}

export const orderSelectedTests = i => {

}

export const selectTest = i => {
  availableTests.update( setById(i))
}

// export const selectTest =  i => {
//   availableTests.update(draft => {
//         draft.find(v=> v.id === i).testSelected = true;
//     });
// };
export const unSelectTest =  i => {
  availableTests.update(draft => {
        draft.find(v=> v.id === i).testSelected = false;
    });
};

