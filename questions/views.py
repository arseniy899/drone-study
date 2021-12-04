from rest_framework import viewsets
from questions.services.serializers import QuestionSerializer
from rest_framework.permissions import AllowAny
from questions.models import Question


class QuestionsViewSet(viewsets.ReadOnlyModelViewSet):
    permission_classes = [AllowAny]
    serializer_class = QuestionSerializer
    queryset = Question.objects.all()
