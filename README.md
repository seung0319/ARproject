[Github 사용 규칙]
- 조 별 Main 브랜치 + 각 팀원별 브랜치 사용

- commit 규칙 
feat : 새로운 기능에 대한 커밋
fix : 버그 수정에 대한 커밋
build : 빌드 관련 파일 수정에 대한 커밋
chore : 그 외 자잘한 수정에 대한 커밋
ci : CI관련 설정 수정에 대한 커밋
docs : 문서 수정에 대한 커밋
style : 코드 스타일 혹은 포맷 등에 관한 커밋
refactor :  코드 리팩토링에 대한 커밋
test : 테스트 코드 수정에 대한 커밋

[유니티 내 폴더관리]
(Assets 하위 폴더명)
01. Scenes
02. Scripts
ㄴ 스크립트 내에도 Scene별로 폴더링
ㄴ 이름은 최대한 직관적으로
ㄴ 모든 스크립트에 코드 주석 필수
(스크립트 첫 줄에는 해당 스크립트의 기능(용도) Summary 작성 필수)
03. Prefabs
04. Resources
ㄴ 01. Images (2D 스프라이트 모음)
ㄴ 02. Models (3D 오브젝트 모음)
ㄴ 03. Materials
ㄴ 04. Sounds
05. Animations

최대한 위 폴더에 맞춰 관리하되, 팀별 추가 폴더가 필요할 경우 자유롭게 추가 가능